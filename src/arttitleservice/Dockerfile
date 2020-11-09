# Use an official Python runtime as a parent image
FROM huggingface/transformers-pytorch-cpu
# Set the working directory to /app
WORKDIR /app
# Copy the current directory contents into the container at /app
COPY ./requirements.txt /app
# Install any needed packages specified in requirements.txt
RUN pip install --trusted-host pypi.python.org -r requirements.txt
EXPOSE $PORT_ARG
HEALTHCHECK CMD curl --fail http://localhost:8080/healthcheck || exit 1 
COPY . /app
CMD ["gunicorn", "-w", "1", "-b", "0.0.0.0:8080", "wsgi:server"]
