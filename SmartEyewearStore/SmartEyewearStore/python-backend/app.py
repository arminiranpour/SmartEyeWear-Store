from flask import Flask, request, jsonify
import cv2
import mediapipe as mp
import numpy as np

app = Flask(__name__)
mp_face_mesh = mp.solutions.face_mesh
face_mesh = mp_face_mesh.FaceMesh(static_image_mode=True)

def analyze_face(image_path):
    image = cv2.imread(image_path)
    if image is None:
        return "Image not found"

    rgb_image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = face_mesh.process(rgb_image)

    if not results.multi_face_landmarks:
        return "No face detected"

    landmarks = results.multi_face_landmarks[0].landmark

    jaw_left = np.array([landmarks[234].x, landmarks[234].y])
    jaw_right = np.array([landmarks[454].x, landmarks[454].y])
    forehead_center = np.array([landmarks[10].x, landmarks[10].y])
    chin = np.array([landmarks[152].x, landmarks[152].y])

    jaw_width = np.linalg.norm(jaw_right - jaw_left)
    face_length = np.linalg.norm(forehead_center - chin)

    ratio = face_length / jaw_width

    if ratio < 1.3:
        return "Round"
    elif 1.3 <= ratio < 1.5:
        return "Square"
    elif 1.5 <= ratio < 1.7:
        return "Oval"
    elif ratio >= 1.7:
        return "Oblong"
    else:
        return "Unknown"

@app.route('/analyze', methods=['POST'])
def analyze():
    file = request.files['image']
    file.save("temp.jpg")
    shape = analyze_face("temp.jpg")
    return jsonify({"face_shape": shape})

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
