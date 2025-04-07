#include <iostream>
#include <memory>
#include <string>

#include <grpcpp/grpcpp.h>
#include "exchange.grpc.pb.h"

using grpc::Channel;
using grpc::ClientContext;
using grpc::Status;
using grpc::ClientReader;
using exchange::ExchangeService;
using exchange::ClientRegistrationRequest;
using exchange::MessageRequest;

class ExchangeClient {
public:
    ExchangeClient(std::shared_ptr<Channel> channel)
        : stub_(ExchangeService::NewStub(channel)) {
    }

    void RegisterClient(const std::string& client_id) {
        ClientRegistrationRequest request;
        request.set_client_id(client_id);

        ClientContext context;
        std::unique_ptr<ClientReader<MessageRequest>> reader(
            stub_->RegisterClient(&context, request));

        MessageRequest message;
        while (reader->Read(&message)) {
            std::cout << "Received message:\n";
            std::cout << "  Sender: " << message.sender() << "\n";
            std::cout << "  Payload: " << message.message_payload() << "\n";
            std::cout << "-------------------------\n";
        }

        Status status = reader->Finish();
        if (!status.ok()) {
            std::cerr << "Register RPC failed: " << status.error_message() << std::endl;
        }
    }

private:
    std::unique_ptr<ExchangeService::Stub> stub_;
};

int main() {
    ExchangeClient client(grpc::CreateChannel("localhost:5215", grpc::InsecureChannelCredentials()));

    std::cout << "Registering client 'ClientA'...\n";
    client.RegisterClient("ClientA");

    return 0;
}
