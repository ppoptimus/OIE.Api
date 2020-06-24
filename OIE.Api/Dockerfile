#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/dotnet:2.2-aspnetcore-runtime

MAINTAINER Ekkachai Kiwsanthia
LABEL maintainer="Ekkachai Kiwsanthia <kiwsanthia@gmail.com>" other="kiwsan"

WORKDIR /app
COPY ./bin/docker .

ENTRYPOINT ["dotnet", "OIE.Api.dll"]

#username: Administrator@oie.com
#password: Administrator01*
