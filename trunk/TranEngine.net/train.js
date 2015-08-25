// global object
TrainEngine = {
    $: function(id) {
        return document.getElementById(id);
    }
	,
    webRoot: '',
    // internationalization (injected into the <head> by TrainBasePage)
    i18n: {
        hasRated: '',
        savingTheComment: '',
        comments: '',
        commentWasSaved: '',
        commentWaitingModeration: '',
        cancel: '',
        filter: '',
        apmlDescription: '',
        beTheFirstToRate: '',
        currentlyRated: '',
        ratingHasBeenRegistered: '',
        rateThisXStars: ''
    }
	,
    setFlag: function(iso) {
        if (iso.length > 0)
            TrainEngine.comments.flagImage.src = TrainEngine.webRoot + "pics/flags/" + iso + ".png";
        else
            TrainEngine.comments.flagImage.src = TrainEngine.webRoot + "pics/pixel.gif";
    }
	,

    // Shows the preview of the comment
    showCommentPreview: function() {
        var oPreview = this.$('preview');
        var oCompose = this.$('compose');

        if (oPreview) oPreview.className = 'selected';
        if (oCompose) oCompose.className = '';
        this.$('commentCompose').style.display = 'none';
        this.$('commentPreview').style.display = 'block';
        this.$('commentPreview').innerHTML = '<img src="' + TrainEngine.webRoot + 'pics/ajax-loader.gif" alt="Loading" />';
        var argument = this.$('commentPreview').innerHTML;
        this.addComment(true);
    }
	,
    composeComment: function() {
        var oPreview = this.$('preview');
        var oCompose = this.$('compose');

        if (oPreview) oPreview.className = '';
        if (oCompose) oCompose.className = 'selected';
        this.$('commentPreview').style.display = 'none';
        this.$('commentCompose').style.display = 'block';
    }
	,
    endShowPreview: function(arg, context) {
        TrainEngine.$('commentPreview').innerHTML = arg;
    }
	,
    toggleCommentSavingIndicators: function(bSaving) {
        TrainEngine.$("btnSaveAjax").disabled = bSaving;
        TrainEngine.$("ajaxLoader").style.display = bSaving ? "inline" : "none";
        TrainEngine.$("status").className = "";
        TrainEngine.$("status").innerHTML = "";
        if (!bSaving) {
            TrainEngine.$('commentPreview').innerHTML = "";
            TrainEngine.composeComment();
        }
    }
    ,
    onCommentError: function(error, context) {
        TrainEngine.toggleCommentSavingIndicators(false);
        error = error || "Unknown error occurred.";
        var iDelimiterPos = error.indexOf("|");
        if (iDelimiterPos > 0) {
            error = error.substr(0, iDelimiterPos);
            // Remove numbers from end of error message.
            while (error.length > 0 && error.substr(error.length - 1, 1).match(/\d/)) {
                error = error.substr(0, error.length - 1);
            }
        }
        alert("Sorry, the following error occurred while processing your comment:\n\n" + error);
    }
	,
    addComment: function(preview) {
        var isPreview = preview == true;
        if (!isPreview) {
            TrainEngine.toggleCommentSavingIndicators(true);
            this.$("status").innerHTML = TrainEngine.i18n.savingTheComment;
        }

        var author = TrainEngine.comments.nameBox.value;
        var email = TrainEngine.comments.emailBox.value;
        var website = TrainEngine.comments.websiteBox.value;
        var country = TrainEngine.comments.countryDropDown ? TrainEngine.comments.countryDropDown.value : "";
        var content = TrainEngine.comments.contentBox.value;
        var notify = TrainEngine.$("cbNotify").checked;
        var captcha = TrainEngine.comments.captchaField.value;
        var replyToId = TrainEngine.comments.replyToId ? TrainEngine.comments.replyToId.value : "";

        var avatarInput = TrainEngine.$("avatarImgSrc");
        var avatar = (avatarInput && avatarInput.value) ? avatarInput.value : "";

        var callback = isPreview ? TrainEngine.endShowPreview : TrainEngine.appendComment;
        var argument = author + "-|-" + email + "-|-" + website + "-|-" + country + "-|-" + content + "-|-" + notify + "-|-" + isPreview + "-|-" + captcha + "-|-" + replyToId + "-|-" + avatar;

        WebForm_DoCallback(TrainEngine.comments.controlId, argument, callback, 'comment', TrainEngine.onCommentError, false);

        if (!isPreview && typeof (OnComment) != "undefined")
            OnComment(author, email, website, country, content);
    }
    ,
    cancelReply: function() {
        this.replyToComment('');
    }
	,
    replyToComment: function(id) {

        // set hidden value
        TrainEngine.comments.replyToId.value = id;

        // move comment form into position
        var commentForm = TrainEngine.$('comment-form');
        if (!id || id == '' || id == null || id == '00000000-0000-0000-0000-000000000000') {
            // move to after comment list
            var base = TrainEngine.$("commentlist");
            base.appendChild(commentForm);
            // hide cancel button
            TrainEngine.$('cancelReply').style.display = 'none';
        } else {
            // show cancel
            TrainEngine.$('cancelReply').style.display = '';

            // move to nested position
            var parentComment = TrainEngine.$('id_' + id);
            var replies = TrainEngine.$('replies_' + id);

            // add if necessary
            if (replies == null) {
                replies = document.createElement('div');
                replies.className = 'comment-replies';
                replies.setAttribute('id') = 'replies_' + id;
                parentComment.appendChild(replies);
            }
            replies.style.display = '';
            replies.appendChild(commentForm);
        }

        TrainEngine.comments.nameBox.focus();
    }
	,
    appendComment: function(args, context) {
        if (context == "comment") {
            var commentList = TrainEngine.$("commentlist");
            if (commentList.innerHTML.length < 10)
                commentList.innerHTML = "<h1 id='comment'>" + TrainEngine.i18n.comments + "</h1>"

            // add comment html to the right place
            var id = TrainEngine.comments.replyToId ? TrainEngine.comments.replyToId.value : '';

            if (id != '') {
                var replies = TrainEngine.$('replies_' + id);
                replies.innerHTML += args;
            } else {
                commentList.innerHTML += args;
                commentList.style.display = 'block';
            }

            // reset form values
            TrainEngine.comments.contentBox.value = "";
            TrainEngine.comments.contentBox = TrainEngine.$(TrainEngine.comments.contentBox.id);
            TrainEngine.toggleCommentSavingIndicators(false);
            TrainEngine.$("status").className = "success";

            if (!TrainEngine.comments.moderation)
                TrainEngine.$("status").innerHTML = TrainEngine.i18n.commentWasSaved;
            else
                TrainEngine.$("status").innerHTML = TrainEngine.i18n.commentWaitingModeration;

            // move form back to bottom
            var commentForm = TrainEngine.$('comment-form');
            commentList.appendChild(commentForm);
            // reset reply to
            if (TrainEngine.comments.replyToId) TrainEngine.comments.replyToId.value = '';
            if (TrainEngine.$('cancelReply')) TrainEngine.$('cancelReply').style.display = 'none';
        }

        TrainEngine.$("btnSaveAjax").disabled = false;
    }
	,
    validateAndSubmitCommentForm: function() {
        var bBuiltInValidationPasses = Page_ClientValidate('AddComment');
        var bNameIsValid = TrainEngine.comments.nameBox.value.length > 0;
        document.getElementById('spnNameRequired').style.display = bNameIsValid ? 'none' : '';
        var bAuthorNameIsValid = true;
        if (TrainEngine.comments.checkName) {
            var author = TrainEngine.comments.postAuthor;
            var visitor = TrainEngine.comments.nameBox.value;
            bAuthorNameIsValid = !this.equal(author, visitor);
        }
        document.getElementById('spnChooseOtherName').style.display = bAuthorNameIsValid ? 'none' : '';
        if (bBuiltInValidationPasses && bNameIsValid && bAuthorNameIsValid) {
            TrainEngine.addComment();
            return true;
        }
        return false;
    }
	,
    addBbCode: function(v) {
        try {
            var contentBox = TrainEngine.comments.contentBox;
            if (contentBox.selectionStart) // firefox
            {
                var pretxt = contentBox.value.substring(0, contentBox.selectionStart);
                var therest = contentBox.value.substr(contentBox.selectionEnd);
                var sel = contentBox.value.substring(contentBox.selectionStart, contentBox.selectionEnd);
                contentBox.value = pretxt + "[" + v + "]" + sel + "[/" + v + "]" + therest;
                contentBox.focus();
            }
            else if (document.selection && document.selection.createRange) // IE
            {
                var str = document.selection.createRange().text;
                contentBox.focus();
                var sel = document.selection.createRange();
                sel.text = "[" + v + "]" + str + "[/" + v + "]";
            }
        }
        catch (ex) { }

        return;
    }
	,
    // Searches the blog based on the entered text and
    // searches comments as well if chosen.
    search: function(root) {
        var input = this.$("searchfield");
        var check = this.$("searchcomments");

        var search = "search.aspx?q=" + encodeURIComponent(input.value);
        if (check != null && check.checked)
            search += "&comment=true";

        top.location.href = root + search;

        return false;
    }
	,
    // Clears the search fields on focus.
    searchClear: function(defaultText) {
        var input = this.$("searchfield");
        if (input.value == defaultText)
            input.value = "";
        else if (input.value == "")
            input.value = defaultText;
    }
	,
    rate: function(id, rating) {
        this.createCallback("rating.axd?id=" + id + "&rating=" + rating, TrainEngine.ratingCallback);
    }
	,
    ratingCallback: function(response) {
        var rating = response.substring(0, 1);
        var status = response.substring(1);

        if (status == "OK") {
            if (typeof OnRating != "undefined")
                OnRating(rating);

            alert(TrainEngine.i18n.ratingHasBeenRegistered);
        }
        else if (status == "HASRATED") {
            alert(TrainEngine.i18n.hasRated);
        }
        else {
            alert("An error occured while registering your rating. Please try again");
        }
    }
	,
    /// <summary>
    /// Creates a client callback back to the requesting page
    /// and calls the callback method with the response as parameter.
    /// </summary>
    createCallback: function(url, callback) {
        var http = TrainEngine.getHttpObject();
        http.open("GET", url, true);

        http.onreadystatechange = function() {
            if (http.readyState == 4) {
                if (http.responseText.length > 0 && callback != null)
                    callback(http.responseText);
            }
        };

        http.send(null);
    }
	,
    /// <summary>
    /// Creates a XmlHttpRequest object.
    /// </summary>
    getHttpObject: function() {
        if (typeof XMLHttpRequest != 'undefined')
            return new XMLHttpRequest();

        try {
            return new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                return new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e) { }
        }

        return false;
    }
	,
    // Updates the calendar from client-callback
    updateCalendar: function(args, context) {
        var cal = TrainEngine.$('calendarContainer');
        cal.innerHTML = args;
        TrainEngine.Calendar.months[context] = args;
    }
	,
    toggleMonth: function(year) {
        var monthList = TrainEngine.$("monthList");
        var years = monthList.getElementsByTagName("ul");
        for (i = 0; i < years.length; i++) {
            if (years[i].id == year) {
                var state = years[i].className == "open" ? "" : "open";
                years[i].className = state;
                break;
            }
        }
    }
	,
    // Adds a trim method to all strings.
    equal: function(first, second) {
        var f = first.toLowerCase().replace(new RegExp(' ', 'gi'), '');
        var s = second.toLowerCase().replace(new RegExp(' ', 'gi'), '');
        return f == s;
    }
	,
    /*-----------------------------------------------------------------------------
    XFN HIGHLIGHTER
    -----------------------------------------------------------------------------*/
    xfnRelationships: ['friend', 'acquaintance', 'contact', 'met'
										, 'co-worker', 'colleague', 'co-resident'
										, 'neighbor', 'child', 'parent', 'sibling'
										, 'spouse', 'kin', 'muse', 'crush', 'date'
										, 'sweetheart', 'me']
	,
    // Applies the XFN tags of a link to the title tag
    hightLightXfn: function() {
        var content = TrainEngine.$('content');
        if (content == null)
            return;

        var links = content.getElementsByTagName('a');
        for (i = 0; i < links.length; i++) {
            var link = links[i];
            var rel = link.getAttribute('rel');
            if (rel && rel != "nofollow") {
                for (j = 0; j < TrainEngine.xfnRelationships.length; j++) {
                    if (rel.indexOf(TrainEngine.xfnRelationships[j]) > -1) {
                        link.title = 'XFN relationship: ' + rel;
                        break;
                    }
                }
            }
        }
    }
	,

    showRating: function(container, id, raters, rating) {
        var div = document.createElement('div');
        div.className = 'rating';

        var p = document.createElement('p');
        div.appendChild(p);
        if (raters == 0) {
            p.innerHTML = TrainEngine.i18n.beTheFirstToRate;
        }
        else {
            p.innerHTML = TrainEngine.i18n.currentlyRated.replace('{0}', new Number(rating).toFixed(1)).replace('{1}', raters);
        }

        var ul = document.createElement('ul');
        ul.className = 'star-rating small-star';
        div.appendChild(ul);

        var li = document.createElement('li');
        li.className = 'current-rating';
        li.style.width = Math.round(rating * 20) + '%';
        li.innerHTML = 'Currently ' + rating + '/5 Stars.';
        ul.appendChild(li);

        for (var i = 1; i <= 5; i++) {
            var l = document.createElement('li');
            var a = document.createElement('a');
            a.innerHTML = i;
            a.href = 'rate/' + i;
            a.className = this.englishNumber(i);
            a.title = TrainEngine.i18n.rateThisXStars.replace('{0}', i.toString()).replace('{1}', i == 1 ? '' : 's');
            a.onclick = function() {
                TrainEngine.rate(id, this.innerHTML);
                return false;
            };

            l.appendChild(a);
            ul.appendChild(l);
        }

        container.innerHTML = '';
        container.appendChild(div);
        container.style.visibility = 'visible';
    }
	,

    applyRatings: function() {
        var divs = document.getElementsByTagName('div');
        for (var i = 0; i < divs.length; i++) {
            if (divs[i].className == 'ratingcontainer') {
                var args = divs[i].innerHTML.split('|');
                TrainEngine.showRating(divs[i], args[0], args[1], args[2]);
            }
        }
    },

    englishNumber: function(number) {
        if (number == 1)
            return 'one-star';

        if (number == 2)
            return 'two-stars';

        if (number == 3)
            return 'three-stars';

        if (number == 4)
            return 'four-stars';

        return 'five-stars';
    }
	,
    // Adds event to window.onload without overwriting currently assigned onload functions.
    // Function found at Simon Willison's weblog - http://simon.incutio.com/
    addLoadEvent: function(func) {
        var oldonload = window.onload;
        if (typeof window.onload != 'function') {
            window.onload = func;
        }
        else {
            window.onload = function() {
                oldonload();
                func();
            }
        }
    }
	,
    filterByAPML: function() {
        var width = document.documentElement.clientWidth + document.documentElement.scrollLeft;
        var height = document.documentElement.clientHeight + document.documentElement.scrollTop;
        document.body.style.position = 'static';

        var layer = document.createElement('div');
        layer.style.zIndex = 2;
        layer.id = 'layer';
        layer.style.position = 'absolute';
        layer.style.top = '0px';
        layer.style.left = '0px';
        layer.style.height = document.documentElement.scrollHeight + 'px';
        layer.style.width = width + 'px';
        layer.style.backgroundColor = 'black';
        layer.style.opacity = '.6';
        layer.style.filter += ("progid:DXImageTransform.Microsoft.Alpha(opacity=60)");
        document.body.appendChild(layer);

        var div = document.createElement('div');
        div.style.zIndex = 3;
        div.id = 'apmlfilter';
        div.style.position = (navigator.userAgent.indexOf('MSIE 6') > -1) ? 'absolute' : 'fixed';
        div.style.top = '200px';
        div.style.left = (width / 2) - (400 / 2) + 'px';
        div.style.height = '50px';
        div.style.width = '400px';
        div.style.backgroundColor = 'white';
        div.style.border = '2px solid silver';
        div.style.padding = '20px';
        document.body.appendChild(div);

        var p = document.createElement('p');
        p.innerHTML = TrainEngine.i18n.apmlDescription;
        p.style.margin = '0px';
        div.appendChild(p);

        var form = document.createElement('form');
        form.method = 'get';
        form.style.display = 'inline';
        form.action = TrainEngine.webRoot;
        div.appendChild(form);

        var textbox = document.createElement('input');
        textbox.type = 'text';
        textbox.value = TrainEngine.getCookieValue('url') || 'http://';
        textbox.style.width = '320px';
        textbox.id = 'txtapml';
        textbox.name = 'apml';
        textbox.style.background = 'url(' + TrainEngine.webRoot + 'pics/apml.png) no-repeat 2px center';
        textbox.style.paddingLeft = '16px';
        form.appendChild(textbox);
        textbox.focus();

        var button = document.createElement('input');
        button.type = 'submit';
        button.value = TrainEngine.i18n.filter;
        button.onclick = function() { location.href = TrainEngine.webRoot + '?apml=' + encodeURIComponent(TrainEngine.$('txtapml').value) };
        form.appendChild(button);

        var br = document.createElement('br');
        div.appendChild(br);

        var a = document.createElement('a');
        a.innerHTML = TrainEngine.i18n.cancel;
        a.href = 'javascript:void(0)';
        a.onclick = function() { document.body.removeChild(TrainEngine.$('layer')); document.body.removeChild(TrainEngine.$('apmlfilter')); document.body.style.position = ''; };
        div.appendChild(a);
    }
	,
    getCookieValue: function(name) {
        var cookie = new String(document.cookie);

        if (cookie != null && cookie.indexOf('comment=') > -1) {
            var start = cookie.indexOf(name + '=') + name.length + 1;
            var end = cookie.indexOf('&', start);
            if (end > start && start > -1)
                return cookie.substring(start, end);
        }

        return null;
    }
	,
    comments: {
        flagImage: null,
        contentBox: null,
        moderation: null,
        checkName: null,
        postAuthor: null,
        nameBox: null,
        emailBox: null,
        websiteBox: null,
        countryDropDown: null,
        captchaField: null,
        controlId: null,
        replyToId: null
    }
};

TrainEngine.addLoadEvent(TrainEngine.hightLightXfn);

// add this to global if it doesn't exist yet
if (typeof ($) == 'undefined')
    window.$ = TrainEngine.$;

if (typeof (registerCommentBox) != 'undefined')
    TrainEngine.addLoadEvent(registerCommentBox);
if (typeof (registerVariables) != 'undefined')
    TrainEngine.addLoadEvent(registerVariables);
if (typeof (setupTrainEngineCalendar) != 'undefined')
    TrainEngine.addLoadEvent(setupTrainEngineCalendar);

// apply ratings after registerVariables.
TrainEngine.addLoadEvent(TrainEngine.applyRatings);

function fnt_over(x1, x2) {
    for (i = 1; i <= 3; i++) {
        var nxspn = document.getElementById("nx" + x1 + "spn" + i);
        var nxul = document.getElementById("nx" + x1 + "ul" + i);
        if (i == x2) { nxspn.className = "over"; nxul.style.display = "block"; }
        else { nxspn.className = ""; nxul.style.display = "none"; }
    }
}