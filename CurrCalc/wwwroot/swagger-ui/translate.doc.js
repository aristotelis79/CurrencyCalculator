
const Language = {

    select_list_id_selector: "languages",
    topBar_selector: ".topbar-wrapper",
    language_Key_selector: "data-lang-key",
    close_block_selector: ".opblock-tag",
    block_summary_selector: ".opblock-summary-description",
    method_summary_selector: ".opblock-summary",
    parameters_selector: ".renderedMarkdown > p,.renderedMarkdown > input",
    data_is_open_selector: 'data-is-open',
    localize_key_start: "Localize_Key_",
    select_list_html: `<label for="languages" style="color:white">&nbsp&nbsp</label>
                        <select onchange="Language.changeLanguageResources(this.value)" id="languages">
                        </select>`,

    languagesUrl: "/languages",
    localizeTextsUrl: "/localizeTexts",

    textElements: {},
    sources: {},

    init: function() {

        //add select list html item
        const listEl = document.createElement("div");
        listEl.innerHTML = Language.select_list_html;
        document.querySelector(Language.topBar_selector).append(listEl);

        //get languages
        Helpers.getData(Language.languagesUrl).then((data) => {
            Helpers.toSelectList(`#${Language.select_list_id_selector}`, data.data);
        });

        Language.setKeys();

        Language.changeLanguageResources('en');

        Language.collapseEvent(Language.close_block_selector);

        Language.collapseEvent(Language.method_summary_selector);
    },

    setKeys: function() {
        Language.textElements = document.querySelectorAll(`${Language.parameters_selector},${Language.block_summary_selector}`);
        Language.textElements.forEach(e => {
            if (e.innerText.startsWith(Language.localize_key_start))
                e.setAttribute(Language.language_Key_selector, e.innerText);
        });
    },

    collapseEvent: function(selector) {
        const blocks = document.querySelectorAll(selector);
        blocks.forEach((b) => {
            b.onclick = function() {
                    setTimeout(function() {
                            Language.setKeys();
                            Language.replaceLanguageSources();
                        },
                        1000);
            };
        });
    },

    changeLanguageResources: function(val) {
        Helpers.getData(`${Language.localizeTextsUrl}/${val}`).then((data) => {
            Language.sources = data.data;
            Language.replaceLanguageSources();
        });
    },

    replaceLanguageSources: function() {
        Language.textElements.forEach(e => {
            const key = e.getAttribute(Language.language_Key_selector);
            if (key)
                e.innerText = Language.sources[key];
        });

    }
};

const Helpers = {

    getData: async function (url = '') {
        const response = await fetch(url, {
            method: 'GET',
            mode: 'cors',
            cache: 'default',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json'
            },
            redirect: 'follow',
            referrerPolicy: 'no-referrer'
        });
        return await response.json();
    },

    toSelectList: function (selector, data) {
        const list = document.querySelector(selector);

        data.forEach(d => {
            const opt = document.createElement("option");
            opt.setAttribute("value", d.value);
            opt.innerHTML = d.text;
            list.append(opt);
        });
    }
}


window.onload = setTimeout(function () {
    Language.init();
}, 2000);
