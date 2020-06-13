# UAF Buen Aire
UAF Buen Aire is an Air Quality prediction tool. "Buen Aire" means "good air" which is something we all need.

## Development Notes

20190919: Created a test ZeroMQ data server that reports hello world based on `http://hintjens.com/blog:42`. The goal is for this server to be a simple sink for obtaining data from Purple Air sensors. Second, we want to monitor the Sense HAT for sensor data and the GPS for location information.

20200204: Added some collaborators to the repository

## Docker Notes

To build the container:

```
docker build --pull --rm -f "buen-aire-docker\Dockerfile" -t uaf-buen-aire:latest "buen-aire-docker"
```

To run the container:

```
docker run -u 1000:1000 -p 8888:8888 -it --rm uaf-buen-aire
```

TODO
----

- [ ] Machine Learning is accomplished with a Jupyter Notebook using `tensorflow/tensorflow` as its basis
- [ ] Buen Aire Svc is a Docker image running the 

Setting up Python for Machine Learning
--------------------------------------

- Install Python
- Configure paths
  - `C:\Users\yourusername\AppData\Local\Programs\Python\Python38`
  - `C:\Users\yourusername\AppData\Local\Python\Python38\Scripts`
  - `C:\Users\yourusername\AppData\Roaming\Python\Python38\Scripts`
- Upgrade Pip
  - `python -m pip install --upgrade pip`
- Install Some Dependencies
  - `python -m pip install jupyter pydot graphviz`
- Install Packages
  - `python -m pip install matplotlib pandas seaborn tensorflow keras tensorflow_addons`
  - `python -m pip install --upgrade git+https://github.com/tensorflow/docs`
