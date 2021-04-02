docker build -t mgkaiser/mssqltest1:latest .
docker push mgkaiser/mssqltest1:latest
helm upgrade --install -f ./charts/mssqltest1/values.yaml mssqltest1 ./charts/mssqltest1 --namespace incomm-poc 
for i in $(docker images --filter=dangling=true --format "{{.ID}}"); do docker rm $i; done

