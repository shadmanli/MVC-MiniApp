"use strict";

let deleteBtn = document.querySelector(".delete-about-btn");

if (deleteBtn) {
    deleteBtn.addEventListener("click", function () {
        let id = parseInt(this.getAttribute("data-id"));

        if (!confirm("Bu məlumatı silmək istədiyinizə əminsiniz?")) return;

        fetch(`/admin/about/delete/${id}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" }
        })
            .then(response => {
                if (response.ok) {
                    // DOM-dan məlumatları silirik
                    document.querySelector("#about-table").remove();
                    deleteBtn.remove();
                    document.querySelector(".btn-primary").remove();

                    // "Create" düyməsi əlavə edirik
                    let createBtn = document.createElement("a");
                    createBtn.href = "/admin/about/create";
                    createBtn.className = "btn btn-success mt-3";
                    createBtn.textContent = "Create";

                    document.querySelector(".container").appendChild(createBtn);

                    alert("Məlumat uğurla silindi.");
                } else {
                    alert("Silinmə zamanı xəta baş verdi.");
                }
            })
            .catch(error => {
                console.error("Xəta:", error);
                alert("Serverlə əlaqə xətası.");
            });
    });
}
