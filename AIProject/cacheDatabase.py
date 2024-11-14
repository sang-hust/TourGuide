import sqlite3

# Kết nối tới database
conn = sqlite3.connect('chatbot_data.db')
cursor = conn.cursor()

# Tạo bảng để lưu trữ câu hỏi và câu trả lời nếu chưa tồn tại
cursor.execute('''
    CREATE TABLE IF NOT EXISTS conversations (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        question TEXT,
        answer TEXT
    )
''')
conn.commit()

# Hàm để lưu câu hỏi và câu trả lời vào database
def save_conversation(question, answer):
    cursor.execute("INSERT INTO conversations (question, answer) VALUES (?, ?)", (question, answer))
    conn.commit()

# Kiểm tra bảng đã tạo hay chưa
cursor.execute("SELECT name FROM sqlite_master WHERE type='table';")
tables = cursor.fetchall()
if tables:
    print("Các bảng trong cơ sở dữ liệu:", tables)
else:
    print("Không có bảng nào trong cơ sở dữ liệu.")

# Ví dụ về cách lưu cuộc trò chuyện
save_conversation("What is your name?", "I am a chatbot.")

# Đóng kết nối
conn.close()
