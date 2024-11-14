from flask import Flask, request, jsonify

app = Flask(__name__)

# Định nghĩa chatbot đơn giản
responses = {
    "hello": "Hi! How can I help you?",
    "how are you": "I'm just a bot, but thanks for asking!",
    "bye": "Goodbye! Have a nice day!"
}


# Hàm xử lý trả lời chatbot
def get_response(message):
    message = message.lower()
    return responses.get(message, "I'm sorry, I don't understand that.")


# Route để nhận yêu cầu POST từ Unity
@app.route('/chat', methods=['POST'])
def chat():
    # Kiểm tra và lấy dữ liệu JSON gửi từ Unity
    try:
        data = request.get_json()  # Lấy dữ liệu JSON từ yêu cầu
        if not data or "message" not in data:
            return jsonify({"error": "Bad request, 'message' key missing"}), 400
    except Exception as e:
        return jsonify({"error": f"Error processing request: {str(e)}"}), 400

    # Lấy tin nhắn của người dùng
    user_message = data.get("message", "")

    # Lấy phản hồi từ chatbot
    bot_response = get_response(user_message)

    # Trả về phản hồi dưới dạng JSON
    return jsonify({"response": bot_response})


if __name__ == '__main__':
    # Chạy Flask server
    app.run(host='0.0.0.0', port=5001)
