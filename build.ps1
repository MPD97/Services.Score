docker build -t service.score . ;
docker tag service.score mateusz9090/score:local ;
docker push mateusz9090/score:local