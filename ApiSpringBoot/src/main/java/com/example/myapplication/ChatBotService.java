package com.example.myapplication;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class ChatBotService {
    public String getAnswer(String question) {
        String answer = "I don't have an answer for that.";
        try {
            // Chạy Python script với câu hỏi
            Process process = Runtime.getRuntime().exec(new String[]{"python3", "chatbot.py", question});
            BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
            answer = reader.readLine();
            process.waitFor();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return answer;
    }
}
