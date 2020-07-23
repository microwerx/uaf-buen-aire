#include <czmq.h>

class BuenAireHttpd
{
	public:
		BuenAireHttpd();
		~BuenAireHttpd();

		void run();
	private:
		zsock_t *stream = nullptr;
};

BuenAireHttpd::BuenAireHttpd() {
	stream = zsock_new_stream(NULL);
	if (!stream) {
		fprintf(stderr, "Unable to create new stream socket\n");
	}
	int rc = zsock_bind(stream, "tcp://*:8080");
	if (rc == -1) {
		fprintf(stderr, "Unable to bind new stream socket\n");
		zsock_destroy(&stream);
		stream = nullptr;
	}
}

BuenAireHttpd::~BuenAireHttpd() {
	if (stream) {
		zsock_destroy(&stream);
	}
}

void BuenAireHttpd::run() {
	if (!stream) return;
	while (true) {
		zframe_t *handle = zframe_recv(stream);
		if (!handle) {
			break;
		}
		char *request = zstr_recv(stream);
		puts(request);
		free(request);

		zframe_send(&handle, stream, ZFRAME_MORE + ZFRAME_REUSE);
		zstr_send(stream,
				"HTTP/1.0 200 OK\r\n"
				"Content-Type: text/html\r\n"
				"\r\n"
				"<html><body>Hello, World!</body></html>");

		zframe_send(&handle, stream, ZFRAME_MORE);
		zmq_send(stream, NULL, 0, 0);
		puts("done");
	}
}

int main(int argc, char **argv)
{
	BuenAireHttpd bahttpd;
	bahttpd.run();	
	return 0;
}
