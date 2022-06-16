var util = {
    init: function () {
        util.registerEvents();
    },
    registerEvents: function () {
    },
    isNullOrEmptyString: function (data) {
       
            if (typeof (data) === 'object') {
                if (JSON.stringify(data) === '{}' || JSON.stringify(data) === '[]') {
                    return true;
                } else if (!data) {
                    return true;
                }
                return false;
            } else if (typeof (data) === 'string') {
                if (!data.trim()) {
                    return true;
                }
                return false;
            } else if (typeof (data) === 'undefined') {
                return true;
            } else {
                return false;
            }
        
    },
    getCurrentDayWithoutTime : function () {
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = 
            (month < 10 ? '0' : '') + month + '/' +
            (day < 10 ? '0' : '') + day + '/'+ d.getFullYear() ;

        return output;
    },
    getFirstDateOfCurrentMonth : function () {
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = 1;

        var output =
            (month < 10 ? '0' : '') + month + '/' +
            (day < 10 ? '0' : '') + day + '/' + d.getFullYear();

        return output;
    },

    removeTimeFromDate : function (data) {
       return data.split(' ')[0];
    }
}
util.init();