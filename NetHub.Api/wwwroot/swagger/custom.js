runWhen(() => !!document.querySelector(".description"), main);


const arrowClosed = `<svg class="arrow" width="20" height="20" aria-hidden="true" focusable="false"><use href="#large-arrow-down" xlink:href="#large-arrow-down"></use></svg>`;
const arrowOpened = `<svg class="arrow" width="20" height="20" aria-hidden="true" focusable="false"><use href="#large-arrow-up" xlink:href="#large-arrow-up"></use></svg>`;
const buttonWithArrow = `<button aria-expanded="true" class="expand-operation" title="Collapse operation">${arrowClosed}</button>`;

const quickDevAuthInput = `<input id="quickDevAuthInput" type="text" value="dev@tacles.net" style="margin: 0 10px 0 0">`;
const quickDevAuthButton = `<button id="quickDevAuthButton" class="btn authorize authorize-btn-neutral" style="padding-left: 20px;" onclick="devAuthorization()"><span style="padding: 0;">Quick dev authorize</span></button>`;

function createTitle(text) {
    return `<span>${text}</span><small></small>`;
}


function main() {
    // initDescription();
    // initGetParameters();
    addDevAuthorization();
}


function initDescription() {
    const description = document.querySelector(".description");
    const headerTriggers = description.querySelectorAll(".renderedMarkdown div>h2");

    headerTriggers.forEach(trigger => {
        const block = trigger.nextElementSibling;
        block.classList.add("hidden");

        trigger.classList.add("opblock-tag");
        trigger.classList.add("no-desc");
        trigger.innerHTML = createTitle(trigger.innerHTML) + buttonWithArrow;

        trigger.onclick = e => {
            const block = trigger.nextElementSibling;
            if (block.classList.contains("hidden")) {
                trigger.innerHTML = createTitle(trigger.innerText) + arrowOpened;
                block.classList.remove("hidden");
            } else {
                trigger.innerHTML = createTitle(trigger.innerText) + arrowClosed;
                block.classList.add("hidden");
            }
        };
    });
}


function initGetParameters() {
    const blockOpeningContainers = document.querySelectorAll(".opblock-get");

    blockOpeningContainers.forEach(container => {
        const trigger = container.querySelector(".opblock-summary-control");

        trigger.onclick = e => {
            runWhen(() => !!container.querySelector(".opblock-section-header"), () => {
                const parametersHeader = container.querySelector(".opblock-section-header");
                const parametersTable = container.querySelector(".parameters-container");
                console.log("header", parametersHeader);
                console.log(parametersTable);

                if (!parametersTable.classList.contains("hidden"))
                    parametersTable.classList.add("hidden");

                const openCloseTrigger = container.querySelector(".opblock-section .opblock-section-header");
                openCloseTrigger.onclick = e => {
                    parametersTable.classList.toggle("hidden");
                };
            });
        }
    });
}

//Все мужики думают как бы перетискать все ладошки других мужиков в помещении

function addDevAuthorization() {
    const authContainer = document.querySelector(".auth-wrapper");
    const devAuthWrapper = document.createElement("div");
    devAuthWrapper.classList.add("auth-wrapper");
    devAuthWrapper.classList.add("custom-authorize-wrapper");
    devAuthWrapper.innerHTML = `${quickDevAuthInput}${quickDevAuthButton}`;

    authContainer.parentNode.append(devAuthWrapper);
    authContainer.parentElement.classList.add("auth-wrapper-wrapper-to-end")
}

function devAuthorization() {
    const input = document.querySelector("#quickDevAuthInput");
    const button = document.querySelector("#quickDevAuthButton");

    createValidationToken(input.value)
        .then(res => res.json())
        .then(res => validateValidationToken(res.id)
            .then(() => createJwtToken(res.id)
                .then(res => res.json())
                .then(success)
                .catch(error))
            .catch(error))
        .catch(error);

    function error(err) {
        console.error(err);
        button.classList.remove("authorize-btn-neutral");
        button.classList.add("authorize-btn-error");
    }

    function success(res) {
        console.log(res);
        button.classList.remove("authorize-btn-neutral");
        button.classList.add("authorize-btn-success");
    }

    function createValidationToken(identifier) {
        return fetch(window.location.origin + "/v1/validation-tokens", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                "target": "email",
                "identifier": identifier,
            })
        });
    }

    function validateValidationToken(validationTokenId) {
        return fetch(window.location.origin + "/v1/validation-tokens", {
            method: "PUT",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                "id": validationTokenId,
                "code": "123456",
            })
        });
    }

    function createJwtToken(validationTokenId) {
        return fetch(window.location.origin + "/v1/jwt-tokens", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                "validationTokenId": validationTokenId,
            })
        });
    }
}


// It takes the document a sec to load the swagger stuff loop until we find it
function runWhen(condition, action) {
    const checkExist = setInterval(() => {
        if (condition()) {
            clearInterval(checkExist);
            action();
        }
    }, 100); // check every 100ms
}