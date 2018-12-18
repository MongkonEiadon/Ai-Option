# Ai-Option (Follower Traders)

         █████╗ ██╗       ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗
        ██╔══██╗██║      ██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
        ███████║██║█████╗██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║
        ██╔══██║██║╚════╝██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║
        ██║  ██║██║      ╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║
        ╚═╝  ╚═╝╚═╝       ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝

To update database use entityframework through Pacage Management Console in Visual Studio by SetStarup project to `iqoption.WebApi`, and applied EF-Migration to `iqoption.data`
```javascript
$ update-database
```

To Run
```
dot net run
```

Kill Running containers
```
docker ps --filter "status=running" --filter "name=aioption_tradings" --format {{.ID}} -n 1

```

Compose-Up
```
docker-compose  -f "C:\git\AiOption\docker-compose.yml" -f "C:\git\AiOption\docker-compose.override.yml" -f "C:\git\AiOption\obj\Docker\docker-compose.vs.debug.g.yml" -p dockercompose12442644972124986181 --no-ansi build 
```