FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY OccBooking.sln ./
COPY OccBooking.Application/*.csproj ./OccBooking.Application/
COPY OccBooking.Auth/*.csproj ./OccBooking.Auth/
COPY OccBooking.Common/*.csproj ./OccBooking.Common/
COPY OccBooking.Domain/*.csproj ./OccBooking.Domain/
COPY OccBooking.Domain.Tests/*.csproj ./OccBooking.Domain.Tests/
COPY OccBooking.Persistance/*.csproj ./OccBooking.Persistance/
COPY OccBooking.Web/*.csproj ./OccBooking.Web/

RUN dotnet restore
COPY . .

WORKDIR /src/OccBooking.Application
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Application.Tests
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Auth
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Common
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Domain.Tests
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Persistance
RUN dotnet build -c Release -o /app

WORKDIR /src/OccBooking.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OccBooking.Web.dll"]