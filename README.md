# Ai-Option (Follower Traders)

         █████╗ ██╗       ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗
        ██╔══██╗██║      ██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
        ███████║██║█████╗██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║
        ██╔══██║██║╚════╝██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║
        ██║  ██║██║      ╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║
        ╚═╝  ╚═╝╚═╝       ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝

To update database use entityframework through Pacage Management Console in Visual Studio by SetStarup project to `AiOption.Tradings`, and applied EF-Migration to `AiOption.Infrasturcture.ReadStores`
```javascript
$ update-database
```

To Run
```
dot net run
```

# The Aggregate
now have 2 types of AggregateRoot
- CustomerAggregateRoot
- IqAccountAggregateRoot
