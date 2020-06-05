FROM ubuntu:20.04
LABEL version="0.1"
LABEL maintainer="microwerx@outlook.com"
LABEL description="Machine Learning with Ubuntu 20.04 with TensorFlow GPU/Jupyter, Keras, PyTorch, and more."

# Packages
# - TensorFlow with GPU and Jupyter Support
# - Pandas
# - Matplotlib

RUN echo 'export PATH=~/.local/bin:$PATH' >> $HOME/.bashrc

RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install -y nano vim-tiny emacs-nox tmux

FROM tensorflow/tensorflow:latest-gpu-jupyter
# FROM pytorch/pytorch:latest

# RUN pip install

# EXPOSE 8080/tcp

CMD [ "bash" ]
