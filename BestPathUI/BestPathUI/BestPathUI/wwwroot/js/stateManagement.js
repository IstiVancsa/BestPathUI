window.stateManager = {
    save: function (key, str) {
        localStorage[key] = str;
        console.log("token saved");
    },
    load: function (key) {
        console.log("token loaded");
        return localStorage[key];
    }
};
