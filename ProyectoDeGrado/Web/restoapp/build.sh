#!/usr/bin/env bash
virtualenv .venv
source .venv/bin/activate
pip install django djangorestframework psycopg2-binary whitenoise Pillow