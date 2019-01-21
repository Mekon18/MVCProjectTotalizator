
function teamChanged(control, id1, id2, coef1, coef2) {
    var obj = {
        FirstTeam: { Id: id1, Coef: coef1 },
        SecondTeam: { Id: id2, Coef: coef2 }
    };

    var cof;
    if (obj.FirstTeam.Id == control.value) {
        cof = obj.FirstTeam.Coef;
    }
    else {
        cof = obj.SecondTeam.Coef;
    }
    var t = $(control).parent().parent();
    var bets = $("#bets").children(".bet");
    var num = bets.index(t);
    var selector = "#coefficient-" + num.toString();
    var html = "Коэффициент: " + cof;
    var s = bets.find(selector);;
    s.html(html);
}

function AddBet() {
    var bets = $("#bets").children(".bet");
    var n = bets.index(bets.last());
    if (isNaN(n)) {
        n = 0;
    }
    n++;
    var resultSelect = $("#betTemplate").children("select");
    resultSelect.attr("name", "Bets[" + n.toString() + "].Bet.ResultType");

    var betbody = $("#betTemplate").children(".bet-body");

    var teamSelect = betbody.children("select");
    teamSelect.attr("name", "Bets[" + n.toString() + "].Bet.ResultValue");

    var moneyInput = betbody.children(".Money");
    moneyInput.attr("name", "Bets[" + n.toString() + "].Bet.Money");


    var validationMessageSpan = betbody.children("#money-valmsg");
    validationMessageSpan.attr("data-valmsg-for", "Bets[" + n.toString() + "].Bet.Money");

    var coefSpan = betbody.children("#coefficient-" + (n - 1));
    coefSpan.attr("id", "coefficient-" + n);

    text = $("#betTemplate").html();
    $("#bets").append("<div id=\"bet-" + n.toString() + "\" class=\"bet\" style=\"margin-top: 5px;\">" + text + "</div>");
}

function ResultChanged(control) {
    var t = $(control).parent().find(".bet-body");
    var bets = $("#bets").children();
    var num = bets.index(t.parent());

    if (control.value == "Team") {
        var betbody = $("#betTemplate").children(".bet-body");

        var teamSelect = betbody.children("select");
        teamSelect.attr("name", "Bets[" + num + "].Bet.ResultValue");
                    
        var moneyInput = betbody.children("#Money");
        moneyInput.attr("name", "Bets[" + num + "].Bet.Money");

        var validationMessageSpan = betbody.children("#money-valmsg");
        validationMessageSpan.attr("data-valmsg-for", "Bets[" + num + "].Bet.Money");

        text = $("#betTemplate").children(".bet-body").html();

        $(t).replaceWith("<div class=\"bet-body\">" + text + "</div>");
    }
    if (control.value == "Score") {
        //var resultInput = $("#ScoreTemplate").children("#Result");
        //resultInput.attr("name", "Bets[" + num + "].ResultValue");

        var resultInput = $("#ScoreTemplate").children("#Result1");
        resultInput.attr("name", "Bets[" + num + "].Score1");

        resultInput = $("#ScoreTemplate").children("#Result2");
        resultInput.attr("name", "Bets[" + num + "].Score2");

        var valmsg = $("#ScoreTemplate").children("#valmsg1");
        valmsg.attr("data-valmsg-for", "Bets[" + num + "].Score1");

        valmsg = $("#ScoreTemplate").children("#valmsg2");
        valmsg.attr("data-valmsg-for", "Bets[" + num + "].Score2");

        var moneyInput = $("#ScoreTemplate").children("#Money");
        moneyInput.attr("name", "Bets[" + num + "].Bet.Money");

        var validationMessageSpan = $("#ScoreTemplate").children("#money-valmsg");
        validationMessageSpan.attr("data-valmsg-for", "Bets[" + num + "].Bet.Money");

        var text = $("#ScoreTemplate").html();

        $(t).replaceWith("<div class=\"bet-body\">" + text + "</div>");
    }
    if (control.value == "Amount") {
        var resultInput = $("#AmountTemplate").children("#Result");
        resultInput.attr("name", "Bets[" + num + "].Bet.ResultValue");

        var validationMessageSpan = $("#AmountTemplate").children("#valmsg");
        validationMessageSpan.attr("data-valmsg-for", "Bets[" + num + "].Bet.ResultValue");

        var moneyInput = $("#AmountTemplate").children("#Money");
        moneyInput.attr("name", "Bets[" + num + "].Bet.Money");

        validationMessageSpan = $("#AmountTemplate").children("#money-valmsg");
        validationMessageSpan.attr("data-valmsg-for", "Bets[" + num + "].Bet.Money");

        var text = $("#AmountTemplate").html();
        $(t).replaceWith("<div class=\"bet-body\">" + text + "</div>");
    }
}

function deleteBet(control) {
    var bets = $(control).parent().nextAll(".bet");
    for (var i = 0; i < bets.length; i++) {
        var id = $(bets[i]).attr("id");
        var num = id[4];

        $(bets[i]).attr("id", "bet-" + (+num - 1));
        $(bets[i]).children("select").attr("name", "Bets[" + (+num - 1) + "].ResultType");

        var betBody = $(bets[i]).children(".bet-body");
        betBody.children("select").attr("name", "Bets[" + (+num - 1) + "].Bet.ResultValue");
        betBody.children("#Result").attr("name", "Bets[" + (+num - 1) + "].Bet.ResultValue");
        betBody.children("#Money").attr("name", "Bets[" + (+num - 1) + "].Bet.Money");
        betBody.children("#Money").attr("id", "Bets_" + (+num - 1) + "__Bet_Money");
        betBody.children("#coefficient-" + num).attr("id", "coefficient-" + (+num - 1));
        betBody.children("#money-valmsg").attr("data-valmsg-for", "Bets[" + (+num - 1) + "].Bet.Money");
        betBody.children("#valmsg1").attr("data-valmsg-for", "Bets[" + (+num - 1) + "].Score1");
        betBody.children("#valmsg2").attr("data-valmsg-for", "Bets[" + (+num - 1) + "].Score2");
    }
    if ($(control).parent().next().attr("type") == "hidden") {
        $(control).parent().prev().remove();
    }
    $(control).parent().remove();
}
