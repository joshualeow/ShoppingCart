window.onload = function () { //Only after DOM is completed
    /* setup event listeners for tasks selection */

    /* setup event listener to intercept click on select_count */
    let elem = document.getElementById("a_item");
    elem.addEventListener('click', OnCountClick);
}

function UpdateSelectStatus() {
    let select_count = Object.keys(selected).length; //How many selected
    let select_elem = document.getElementById("select_count"); //pass to cshtml
    let selectbox_elem = document.getElementById("select_count_box");

    if (select_elem && selectbox_elem) {
        if (select_count > 0) {

            select_elem.innerHTML =  select_count 
                ((select_count > 1) ? "s" : "");
        }
        else {
            select_count.style.display = "none";
        }
    }
}

function OnCountClick(event) {
    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Task/AddTasks");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE) {
            if (this.status != 200) {
                /* error; info user etc */
                return;
            }

            let data = JSON.parse(this.responseText);
            if (data.status == "success") {
                window.location.href = "/Task/MyTasks";
            }
        }
    }

    /* package as JSON object */
    let data = { TaskIds: [] };
    for (let key of Object.keys(selected)) {
        data.TaskIds.push(key);
    }

    xhr.send(JSON.stringify(data));
}
