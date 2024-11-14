package com.example.myapplication;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.beans.factory.annotation.Autowired;

@RestController
@RequestMapping("/api/chatbot")
public class ChatBotController {

    @Autowired
    private ChatBotService chatbotService;

    @PostMapping("/ask")
    public ResponseEntity<String> askQuestion(@RequestBody String question) {
        String answer = chatbotService.getAnswer(question);
        return ResponseEntity.ok(answer);
    }
}
