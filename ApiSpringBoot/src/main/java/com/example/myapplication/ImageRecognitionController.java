package com.example.myapplication;

import org.springframework.core.io.Resource;
import org.springframework.core.io.UrlResource;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.io.File;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

@RestController
@RequestMapping("/api")
public class ImageRecognitionController {

    // Thư mục lưu hình ảnh
    private static final String UPLOAD_DIR = "uploaded_images/";

    // API nhận ảnh từ client
    @PostMapping("/uploadImage")
    public ResponseEntity<String> uploadImage(@RequestBody byte[] imageBytes) {
        try {
            // Tạo thư mục nếu chưa có
            File uploadDir = new File(UPLOAD_DIR);
            if (!uploadDir.exists()) {
                uploadDir.mkdirs();
            }

            // Tạo tên file dựa trên timestamp
            String fileName = "image_" + System.currentTimeMillis() + ".png";

            // Lưu file vào thư mục
            Path imagePath = Paths.get(UPLOAD_DIR + fileName);
            Files.write(imagePath, imageBytes);

            // Trả về URL để truy cập hình ảnh
            String imageUrl = "http://localhost:8088/api/images/" + fileName;
            return ResponseEntity.ok(imageUrl);
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.status(500).body("Error processing image");
        }
    }

    // API để lấy ảnh dựa trên tên file
    @GetMapping("/images/{fileName}")
    public ResponseEntity<Resource> getImage(@PathVariable String fileName) {
        try {
            // Lấy file từ thư mục
            Path filePath = Paths.get(UPLOAD_DIR + fileName);
            Resource resource = new UrlResource(filePath.toUri());

            if (resource.exists() || resource.isReadable()) {
                // Trả về ảnh với Content-Type là image/png
                return ResponseEntity.ok()
                        .contentType(MediaType.IMAGE_PNG)
                        .body(resource);
            } else {
                return ResponseEntity.status(404).body(null);
            }
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.status(500).body(null);
        }
    }
}
