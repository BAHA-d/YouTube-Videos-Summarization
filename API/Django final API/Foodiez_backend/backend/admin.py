from django.contrib import admin
from backend.models import transcriptModel
# Register your models here.

MyModels = [transcriptModel]
admin.site.register(MyModels)
