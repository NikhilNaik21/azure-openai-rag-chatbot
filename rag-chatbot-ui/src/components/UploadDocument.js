import React, { useState } from "react";
import { uploadPDF } from "../services/apiService";

function UploadDocument({ onUploadSuccess }) {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleFileChange = async (e) => {
    const file = e.target.files[0];

    if (!file) return;

    if (file.type !== "application/pdf") {
      setError("Only PDF files are allowed.");
      e.target.value = "";
      return;
    }

    //keep upload size very small
    if (file.size > 1024 * 1024) {
      setError("Please upload PDF smaller than 1 MB");
      e.target.value = "";
      return;
    }

    setError(""); //clear any previous error
    setLoading(true);

    try {
      await uploadPDF(file);

      setError("");
      e.target.value = "";

      alert("PDF uploaded successfully");

      onUploadSuccess(file.name);
    } catch (err) {
      setError(err.response?.data || "Upload failed. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="upload-container">
      <input
        type="file"
        accept=".pdf"
        onChange={handleFileChange}
        disabled={loading}
      />
      {error && <p className="error-message">{error}</p>}
      {loading && <p>Uploading...</p>}
    </div>
  );
}

export default UploadDocument;
