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
RUN apt-get install -y nano vim-tiny tilde tmux wget curl git
RUN apt-get install --no-install-recommends -y -q \
    python3 \
    python3-dev \
    python3-setuptools \
    python3-zmq


RUN /usr/bin/python3 -m pip install --upgrade pip

RUN pip install -q matplotlib
RUN pip install -q tensorflow-hub
RUN pip install -q tfds-nightly
RUN pip install -q seaborn
RUN pip install -q git+https://github.com/tensorflow/docs
RUN pip install -q pyyaml h5py
RUN pip install -q keras-tuner
RUN pip install -q torch
RUN pip install -q scikit-image

# RUN pip install

EXPOSE 8888

RUN mkdir /tensorflow_datasets
RUN chown 1000:1000 /tensorflow_datasets
RUN chown 1000:1000 /tf

WORKDIR /tf
COPY notebooks/siggraph2020 siggraph2020
