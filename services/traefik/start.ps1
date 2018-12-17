cd traefik

docker run -d -p 8080:8080 -p 80:80 -u ContainerAdministrator -v //./pipe/docker_engine://./pipe/docker_engine --network traefik-net bnd/traefik --docker.endpoint=npipe:////./pipe/docker_engine --loglevel=DEBUG 
