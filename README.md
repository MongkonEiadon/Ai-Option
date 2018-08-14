# Ai-Option (Follower Traders)

         █████╗ ██╗       ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗
        ██╔══██╗██║      ██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
        ███████║██║█████╗██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║
        ██╔══██║██║╚════╝██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║
        ██║  ██║██║      ╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║
        ╚═╝  ╚═╝╚═╝       ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝
        
        
## Introduction
This is my personal project, to work with [Iqoption.NET](https://github.com/MongkonEiadon/iqoption.net) that I created, background working with CQRS-ES Framework by [EventFlow](https://github.com/eventflow/EventFlow) and combine with AzureServiceBus to communicate between diffrent application


To update database use entityframework through Pacage Management Console in Visual Studio by SetStarup project to `iqoption.WebApi`, and applied EF-Migration to `iqoption.data`
```javascript
$ update-database
```

To Run
```
dot net run
```
