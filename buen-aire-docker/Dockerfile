FROM tensorflow/tensorflow:latest-gpu-jupyter
#FROM ubuntu:20.04
LABEL version="0.1"
LABEL maintainer="microwerx@outlook.com"
LABEL description="Machine Learning with TensorFlow GPU/Jupyter, Keras, PyTorch, and more."

# Packages
# - TensorFlow with GPU and Jupyter Support
# - Pandas
# - Matplotlib

RUN echo 'export PATH=~/.local/bin:$PATH' >> $HOME/.bashrc

RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install -y nano vim-tiny emacs-nox tmux wget curl

# FROM tensorflow/tensorflow:latest-gpu-jupyter

RUN /usr/bin/python3 -m pip install --upgrade pip
RUN pip install -q tensorflow-hub
RUN pip install -q tfds-nightly
RUN pip install -q seaborn
RUN pip install -q git+https://github.com/tensorflow/docs
RUN pip install -q pyyaml h5py
RUN pip install -q -U keras-tuner

# FROM pytorch/pytorch:latest

# RUN pip install

EXPOSE 8080
EXPOSE 8888
EXPOSE 6006
EXPOSE 5000
EXPOSE 5001

VOLUME ./notebooks /notebooks

WORKDIR /tf
COPY notebooks/purpleair.ipynb purpleair.ipynb
COPY notebooks/test.txt test.txt
