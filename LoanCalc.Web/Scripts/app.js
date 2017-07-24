var app = app || {};

app.LoanCalculator = function (cfg) {

    var viewModel = {
        amount: ko.observable("650000"),
        period: ko.observable("20"),
        calculate: function () {
            getSchedule();
        },
        payments: ko.observableArray([]),
        isScheduleVisible: ko.observable(false),
        isErrorMessageVisible: ko.observable(false),
        isLoading: ko.observable(false),
        appInited: ko.observable(true)
    }

    var getSchedule = function() {
        
        var url = cfg.urls.getSchedule +
            "?PaybackTimeInYears=" +viewModel.period() +
            "&Amount=" + viewModel.amount();

        viewModel.isErrorMessageVisible(false);
        viewModel.isLoading(true);
        viewModel.isScheduleVisible(false);

        $.ajax({
            url: url,
            success: function (data) {
                if (data && data.Payments && data.Payments.length > 0) {
                    viewModel.payments(data.Payments);
                    viewModel.isScheduleVisible(true);
                } else {
                    viewModel.isScheduleVisible(false);
                }
                viewModel.isLoading(false);
            },
            error:function() {
                viewModel.payments([]);
                viewModel.isScheduleVisible(false);
                viewModel.isErrorMessageVisible(true);
                viewModel.isLoading(false);
            }
        });
    }

    this.init = function() {
        ko.applyBindings(viewModel);

        getSchedule();
    }
}

$(function () {
    app.loanCalc = new app.LoanCalculator(app.cfg);
    app.loanCalc.init();
});