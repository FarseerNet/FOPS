ver='1.1.0'
dotnet publish -c release
cd bin/release/net6.0/publish
docker build -t farseernet/fops:${ver} --network=host .
docker push farseernet/fops:${ver}

docker tag farseernet/fops:${ver} farseernet/fops:latest
docker push farseernet/fops:latest