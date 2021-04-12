#!/bin.bash

# Get the latest code from origin
git pull origin master

# Build the docker image
docker build -t mgkaiser/mssqltest1:latest .

# Push the docker image to the repository (DockerHub in this case)
docker push mgkaiser/mssqltest1:latest

# Execute the Helm chart
helm upgrade --install -f ./charts/mssqltest1/values.yaml mssqltest1 ./charts/mssqltest1 --namespace incomm-poc 

# Purge any temporary images created while creating the image
for i in $(docker images --filter=dangling=true --format "{{.ID}}"); do docker rm $i; done

