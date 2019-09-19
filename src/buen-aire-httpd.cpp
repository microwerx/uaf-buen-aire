#include <czmq.h>

class BuenAireHttpd
{
	public:
		BuenAireHttpd();
		~BuenAireHttpd();

		void run();
	private:
		zctx_t *ctx = nullptr;
		void *stream = nullptr;
};

BuenAireHttpd::BuenAireHttpd() {
	ctx = zctx_new();
	stream = zsocket_new(ctx, ZMQ_STREAM);
	int rc = zsocket_bind(stream, "tcp://*:8080");
	assert(rc != -1);
	if (rc != -1) {
		stream = nullptr;
	}
}

BuenAireHttpd::~BuenAireHttp() {
	zctx_destroy(&ctx);
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
				"Content-Type: text/plain\r\n"
				"\r\n"
				"Hello, World!");

		zframe_send(&handle, stream, ZFRAME_MORE);
		zmq_send(stream, NULL, 0, 0);
	}
}

int main(int argc, char **argv)
{
	BuenAireHttpd bahttpd;
	bahttpd.run();	
	return 0;
}
