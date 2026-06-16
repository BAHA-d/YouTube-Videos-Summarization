from rest_framework import generics
from backend import serializers
from rest_framework.views import APIView
from rest_framework.status import HTTP_200_OK, HTTP_400_BAD_REQUEST
from rest_framework.response import Response
from rest_framework.generics import ListAPIView, CreateAPIView, DestroyAPIView, UpdateAPIView

#model libraries
import os
from pytubefix  import YouTube
from pydub import AudioSegment
from pydub.utils import make_chunks
import whisper
import numpy as np
import json

#Summurize library
import spacy
from spacy.lang.en.stop_words import STOP_WORDS
from string import punctuation
from heapq import nlargest 




def get_audio_youtube(url):
    yt = YouTube(url)
    video = yt.streams.filter(only_audio=True).first()
    destino = "temp_audio"
    out_file = video.download(output_path=destino)
    base, ext = os.path.splitext(out_file)
    # Read audio file from saved directory
    audio = AudioSegment.from_file(out_file)
    #Remove file from server
    os.remove(out_file)
    return audio


def splitAduio(URLaudio,chunkLength=20000):
    audio = URLaudio.set_frame_rate(16000)
    chunks = make_chunks(audio,chunk_length = chunkLength)
    return chunks

def audiosegment_to_librosawav(audiosegment):    
    audio = audiosegment.set_channels(1) # to mono audio
    y = np.array(audio.get_array_of_samples()).astype(np.float32)
    y = y / (1 << 8*2 - 1)  
    return y

def Transcript_Youtube_URL(youtubeURL):
    fullText = ""
    audio = get_audio_youtube(youtubeURL)
    audioList = splitAduio(audio)
    model = whisper.load_model("base")
    for chunk in audioList:
        segment =audiosegment_to_librosawav(chunk)
        audio = whisper.pad_or_trim(segment)
        mel = whisper.log_mel_spectrogram(audio).to(model.device)
        options = whisper.DecodingOptions(fp16 = False)
        result = whisper.decode(model, mel, options)
        fullText = fullText + " " +  result.text
    fullText = fullText[1::]
    return fullText

def summarize(text, per):
    nlp = spacy.load('en_core_web_sm')
    doc= nlp(text)
    tokens=[token.text for token in doc]
    word_frequencies={}
    for word in doc:
        if word.text.lower() not in list(STOP_WORDS):
            if word.text.lower() not in punctuation:
                if word.text not in word_frequencies.keys():
                    word_frequencies[word.text] = 1
                else:
                    word_frequencies[word.text] += 1
    max_frequency=max(word_frequencies.values())
    for word in word_frequencies.keys():
        word_frequencies[word]=word_frequencies[word]/max_frequency
    sentence_tokens= [sent for sent in doc.sents]
    sentence_scores = {}
    for sent in sentence_tokens:
        for word in sent:
            if word.text.lower() in word_frequencies.keys():
                if sent not in sentence_scores.keys():                            
                    sentence_scores[sent]=word_frequencies[word.text.lower()]
                else:
                    sentence_scores[sent]+=word_frequencies[word.text.lower()]
    select_length=int(len(sentence_tokens)*per)
    summary=nlargest(select_length, sentence_scores,key=sentence_scores.get)
    final_summary=[word.text for word in summary]
    summary=''.join(final_summary)
    return summary

class transcriptYotube(CreateAPIView):

    serializer_class = serializers.transcriptSerializer
    def post(self, request):
       serializer = serializers.transcriptSerializer(data=request.data)
       if serializer.is_valid(raise_exception=True):
            valid_data = serializer.data
            ytURL = valid_data['youtubeURL']
            transcript = Transcript_Youtube_URL(ytURL)
            valid_data['transcript'] = transcript
            valid_data['transcriptSummary'] = summarize(transcript, 0.4)
            
            # #create question
            # Questions = generateQuestions(transcript,10)
          
            # valid_data['Questions'] = json.dumps(Questions)
            #end create
            return Response(valid_data, status=HTTP_200_OK)
       