## This generate the fsp file to pass to fusion
dotnet fusion subgraph pack -w BE.TradeeHub.PriceBookService.Application


## This will add the code from the CustomerServiceSubgraph.fsp file into the fusion project in the gateway.fsp so that it has the types and structure of this project 
dotnet fusion compose -p ../BE.TradeeHub.Fusion/BE.TradeeHub.Fusion/gateway -s BE.TradeeHub.PriceBookService.Application

