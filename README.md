


inorder to kill port:
lsof -t -i:8501
kill xxxxx


portfolio server - http://3.16.148.26:5000/swagger

first time see chat gpt
to start instance
cd /home/ubuntu/portfolio/publish
ls
dotnet portfolioBackend.dll --urls "http://0.0.0.0:5000"

now add ur laptop ip to security grps

any modification in files -> test locally -> push
cd /home/ubuntu/portfolio/portfoliobackend
git pull origin master
cd portfolioBackend
dotnet publish -c Release -o /home/ubuntu/portfolio/publish
dotnet portfolioBackend.dll --urls "http://0.0.0.0:5000"
