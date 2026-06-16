from django.db import models



class transcriptModel(models.Model):
    youtubeURL = models.TextField()
    transcript = models.TextField(null=True)
    transcriptSummary = models.TextField(null=True)
  
    def __str__(self):
        return self.youtubeURL
