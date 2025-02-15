// Hiệu ứng khi tải trang
document.addEventListener("DOMContentLoaded", function () {
    const tableRows = document.querySelectorAll(".table tbody tr");
    tableRows.forEach((row, index) => {
        row.style.opacity = "0";
        row.style.transform = "translateY(20px)";
        setTimeout(() => {
            row.style.opacity = "1";
            row.style.transform = "translateY(0)";
            row.style.transition = "all 0.5s ease-in-out";//chuyen dong muot ,
            //(y) dua hang ve vitridau, 1 hien hang,20px kc xuong20x
        }, index * 150); //(150ms * số thứ tự hàng) tao do tre
    });
});

// Hiệu ứng khi hover vào hàng (mau khi hover va sau khi k hover)
document.querySelectorAll(".table tbody tr").forEach((row) => {
    row.addEventListener("mouseover", () => {
        row.style.backgroundColor = "#ffecb3";
    });
    row.addEventListener("mouseout", () => {
        row.style.backgroundColor = "";
    });
});
