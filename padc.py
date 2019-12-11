import numpy as np
import urllib.request
import json
import matplotlib.pyplot as plt
import cartopy.crs as ccrs
import cartopy.feature as cfeature

EXTENTS = [-172, -128, 40, 90]


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

    def plot(self, plt, style: str):
        if self.good():
            plt.plot(self.lon, self.lat, style)
        else:
            print('Error! --> ' + str(self))

    def good(self):
        return self.lon != None and self.lat != None

    def __str__(self):
        return str(self.id) + ' ' + str(self.lat) + ' ' + str(self.lon) + ' ' + str(self.pm)


class PAData:
    def __init__(self, url: str):
        self.download(url)

    def print(self):
        for d in self.dataPoints:
            print(d)

    def download(self, url: str):
        try:
            response = urllib.request.urlopen(url)
        except HTTPError as e:
            print('Error code: ', e.code)
            return
        http_data = response.read()
        json_data = json.loads(http_data)
        self.process(json_data)

    def process(self, data):
        self.version = data.get('version')
        self.fields = data.get('fields')
        self.count = data.get('count')
        self.dataPoints = []
        fout = open('padata.txt', 'w')
        for d in data.get('data'):
            dp = PADataPoint(d)
            if dp.lat == None or dp.lon == None:
                continue
            if (dp.lat < EXTENTS[2] or dp.lat > EXTENTS[3]):
                continue
            if (dp.lon < EXTENTS[0] or dp.lon > EXTENTS[1]):
                continue
            self.dataPoints.append(dp)
            fout.write(str(dp) + '\n')
        fout.close()
        print("Added " + str(len(self.dataPoints)) + " data points")

    def plot(self, plt, style):
        for d in self.dataPoints:
            d.plot(plt, style)


def plotAlaska():
    ax = plt.axes(projection=ccrs.PlateCarree())
    # ax = plt.axes(projection=ccrs.Orthographic(-153.369141, 66.160507))
    ax.set_extent([-172, -128, 40, 90])
    ax.coastlines()
    ax.stock_img()
    ax.gridlines()

    states_provinces = cfeature.NaturalEarthFeature(
        category='cultural',
        name='admin_1_states_provinces_lines',
        scale='50m',
        facecolor='none')

    ax.add_feature(cfeature.LAND)
    ax.add_feature(cfeature.COASTLINE)
    ax.add_feature(states_provinces, edgecolor='gray')


pad = PAData('https://www.purpleair.com/data.json')

plotAlaska()
pad.plot(plt, 'r+')
plt.show()