"use strict";


document.addEventListener("DOMContentLoaded", function () {
    let deleteTeamBtns = document.querySelectorAll(".delete-team-btn");

    deleteTeamBtns.forEach(btn => {
        btn.addEventListener("click", function () {
            let id = parseInt(this.getAttribute("data-id"));

            fetch(`/Admin/Team/Delete/${id}`, {
                method: "POST"
            })
                .then(response => {
                    if (response.ok) {
                        btn.parentNode.parentNode.remove();
                    } else {
                        alert("Silinmə uğursuz oldu.");
                    }
                })
                .catch(err => console.error(err));
        });
    });
});


let deleteBtn = document.querySelector(".delete-about-btn");

if (deleteBtn) {
    deleteBtn.addEventListener("click", function () {
        const id = parseInt(this.getAttribute("data-id"));

        fetch(`/Admin/About/Delete?id=${id}`, {
            method: 'POST'
        })
            .then(response => {
                if (response.ok) {
                    // DOM-dan sil
                    document.querySelector("#about-table")?.remove();
                    deleteBtn.remove();
                    document.querySelector(".btn-primary")?.remove();

                    // "Create" düyməsi əlavə et
                    let createBtn = document.createElement("a");
                    createBtn.href = "/Admin/About/Create";
                    createBtn.className = "btn btn-success mt-3";
                    createBtn.textContent = "Create";
                    document.querySelector(".container").appendChild(createBtn);

                    alert("Məlumat uğurla silindi.");
                } else {
                    alert("Silinmə uğursuz oldu.");
                }
            })
            .catch(err => {
                console.error(err);
                alert("Server xətası.");
            });
    });
}

document.addEventListener("DOMContentLoaded", function () {
    let deleteSliderBtns = document.querySelectorAll(".delete-slider-btn");

    deleteSliderBtns.forEach(btn => {
        btn.addEventListener("click", function () {
            let id = parseInt(this.getAttribute("data-id"));

            fetch(`/Admin/SliderInfo/Delete/${id}`, {
                method: "POST"
            })
                .then(response => {
                    if (response.ok) {
                        btn.parentNode.parentNode.remove();
                    } else {
                        alert("Silinmə uğursuz oldu.");
                    }
                })
                .catch(err => console.error(err));
        });
    });
});
