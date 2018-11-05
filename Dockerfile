FROM microsoft/dotnet:2.1-sdk as builder
COPY . /
WORKDIR /API
RUN dotnet restore --no-cache
RUN dotnet publish --output /app/ -c Release --no-restore

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=builder /app .

ENV JWT_ISS http://localhost:5000/api/signup
ENV JWT_AUD clinfy.api
ENV JWT_EXPIRATION 1800
ENV MONGO_URI mongodb://mongodb:27017/clinfy
ENV ASPNETCORE_ENVIRONMENT Production

EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]