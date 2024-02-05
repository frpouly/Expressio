FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /Expressio

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /Expressio
COPY --from=build-env /Expressio/out .

# Expose the port your application will run on
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "Expressio.dll"]