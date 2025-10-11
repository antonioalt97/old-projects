from django.urls import path
from .views import create_reserve, delete_reserve, contact_message, modificar_reserva, modify_user
from django.conf import settings
from django.conf.urls.static import static

from . import views

urlpatterns = [
    path('v1/api/create_reserve/', create_reserve),
    path('v1/api/delete_reserve/', delete_reserve),
    path('v1/api/contacto/', contact_message, name='contacto'),
    path('v1/api/modificar_reserva/<int:pk>/', modificar_reserva, name='modificar_reserva'),
    path('v1/api/modify_client/', modify_user, name='modify_user'),
]

urlpatterns += static(settings.MEDIA_URL,document_root=settings.MEDIA_ROOT)