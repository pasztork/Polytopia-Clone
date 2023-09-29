FROM python:3.10-alpine3.18

ENV SERVER_URL=0
ENV PLAYER_NAME=Cersei

WORKDIR /app

COPY . .
RUN pip install --no-cache-dir -r requirements.txt
RUN pip install websocket==0.2.1
RUN pip install websocket-client==1.6.2

EXPOSE 53658

CMD ["python", "./src/app.py"]