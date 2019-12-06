import numpy
import json
from io import StringIO
import urllib.request

dataSource = 'https://www.purpleair.com/data.json'
response = urllib.request.urlopen(dataSource)
data = response.read()
d = json.loads(data)
count = 1
for i in d:
    print("i: " + str(count) + i)
    count += 1
