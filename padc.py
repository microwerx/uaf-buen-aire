import numpy
import json
import matplotlib.pyplot as plt
import numpy as np

from io import StringIO
import urllib.request

class TSDataPoint:
    def __init__(self, ID: int):
        self.ID = ID
        # self.pm010 = channelA.field1
        # self.pm025 = channelA.field2
        # self.pm100 = channelA.field3
        # self.uptimeInMinutes = channelA.field4
        # self.rssi = channelA.field5
        # self.temperatureF = channelA.field6
        # self.humidityPct = channelA.field7
        # self.pm25_ug_m3 = channelA.field8

class PADataPoint:
    # sensor A data fields

    # sensor B data fields

    def __init__(self, data):
        # "ID","pm","age","pm_0","pm_1","pm_2","pm_3","pm_4","pm_5","pm_6","conf","pm1","pm_10","p1","p2","p3","p4","p5","p6","Humidity","Temperature","Pressure","Elevation","Type","Label","Lat","Lon","Icon","isOwner","Flags","Voc","Ozone1","Adc","CH"
        self.id = data[0]
        self.pm = data[1]
        self.age = data[2]
        self.pm_0 = data[3]
        self.pm_1 = data[4]
        self.pm_2 = data[5]
        self.pm_3 = data[6]
        self.pm_4 = data[7]
        self.pm_5 = data[8]
        self.pm_6 = data[9]
        self.conf = data[10]
        self.pm1 = data[11]
        self.pm_10 = data[12]
        self.p1 = data[13]
        self.p2 = data[14]
        self.p3 = data[15]
        self.p4 = data[16]
        self.p5 = data[17]
        self.p6 = data[18]
        self.humidity = data[19]
        self.temperature = data[20]
        self.pressure = data[21]
        self.elevation = data[22]
        self.type = data[23]
        self.label = data[24]
        self.lat = data[25]
        self.lon = data[26]
        self.icon = data[27]
        self.isOwner = data[28]
        self.flags = data[29]
        self.voc = data[30]
        self.ozone1 = data[31]
        self.adc = data[32]
        self.ch = data[33]

    def __str__(self):
        return str(self.id) + ' ' + str(self.lat) + ' ' + str(self.lon) + ' ' + str(self.pm)

class PAData:
    def __init__(self, data):
        self.version = data.get('version')
        self.fields = data.get('fields')
        self.count = data.get('count')
        self.dataPoints = []
        for d in data.get('data'):
            self.dataPoints.append(PADataPoint(d))

    def print(self):
        for d in self.dataPoints:
            print(d)

def is_inside_alaska():
    return True;

def read_current_purple_air(url: str):
    response = urllib.request.urlopen(url)
    http_data = response.read()
    json_data = json.loads(http_data)
    return PAData(json_data)

# pad = read_current_purple_air('https://www.purpleair.com/data.json')
# pad.print()