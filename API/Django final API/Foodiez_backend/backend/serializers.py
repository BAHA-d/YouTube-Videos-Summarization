from rest_framework import serializers
from rest_framework_simplejwt.tokens import RefreshToken
from django.contrib.auth import get_user_model
from backend.models import transcriptModel
User = get_user_model()



class transcriptSerializer(serializers.ModelSerializer):
    class Meta:
        model = transcriptModel
        fields= ["youtubeURL","transcript","transcriptSummary"]
       
       


