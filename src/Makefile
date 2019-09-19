TARGET = bahttpd
SOURCES = buen-aire-httpd.cpp
HEADERS = $(wildcard *.h*)
OBJECTS = $(SOURCES:%.cpp=%.o)

CXXFLAGS = -g -Wall -std=c++14
CXX = g++
LDFLAGS = -lzmq3

.PHONY: all clean

all: $(TARGET)

clean:
	$(RM) $(OBJECTS)
	$(RM) $(TARGET)

$(TARGET): $(OBJECTS)
	$(CXX) -o $@ $(OBJECTS) $(LDFLAGS)

%.o : %.cpp $(HEADERS)
	$(CXX) -o $@ -c $< $(CXXFLAGS)


