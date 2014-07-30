
var questions = [
{
	id: 1,
	questionText: 'Colleague needs to finish an urgent project before they leave. What do you do next?',
	questionMedia: [
		{
			type: 'video',
			url: 'www.youtube.com/watch?v=AlQvFI-ezoc'
		},
		{
			type: 'image',
			url: 'img/Peninsula.jpg'
		}
	],
	questionAnswers: [
		{
			isCorrect: false,
			text: 'a) Suggest they put the information on a USB stick and complete the work on the journey'
		},
		{
			isCorrect: true,
			text: 'b) Stay and help complete the task together'
		},
		{
			isCorrect: false,
			text: 'c) Leave it to them, they can always catch a later train'
		},
		{
			isCorrect: false,
			text: 'd) Suggest they email their manager to say the project won’t be completed on time'
		}
	]
},
{
	id: 2,
	questionText: 'The train to Euston has been cancelled with no notification of the next train.  What do you do next?',
	questionMedia: [
		{
			type: 'image',
			url: 'img/Manchester.jpg'
		},
		{
			type: 'video',
			url: 'www.youtube.com/watch?v=AU-PA8Kqov4'
		}
	],
	questionAnswers: [
		{
			isCorrect: true,
			text: 'a) Ask a guard for their advice on the quickest route'
		},
		{
			isCorrect: false,
			text: 'b) Hang around and see if the board gets updated with new times'
		},
		{
			isCorrect: false,
			text: 'c) Rearrange your flights for the next day'
		},
		{
			isCorrect: false,
			text: 'd) There’s a train to Birmingham on the next platform – jump on it and take it from there'
		}
	]
},
{
	id: 3,
	questionText: 'You bump into a couple that have booked to go to Singapore but haven’t got a hotel yet. What do you do next?',
	questionMedia: [
		{
			type: 'image',
			url: 'img/London.jpg'
		},
		{
			type: 'video',
			url: 'www.youtube.com/watch?v=EOttM05W3b4'
		}
	],
	questionAnswers: [
		{
			isCorrect: false,
			text: 'a) Tell them where you’re staying and suggest they go there'
		},
		{
			isCorrect: false,
			text: 'b) I’m too busy to help – I need to check in my bags'
		},
		{
			isCorrect: false,
			text: 'c) Suggest they search on the internet'
		},
		{
			isCorrect: true,
			text: 'd) Show them how to download the AsiaRooms App'
		}
	]
},
{
	id: 4,
	questionText: 'You are only in Singapore for 1 day and you and your friend have a long list of things to see but might not have enough time. What do you do next?',
	questionMedia: [
		{
			type: 'image',
			url: 'img/Singapore.jpg'
		}
	],
	questionAnswers: [
		{
			isCorrect: false,
			text: 'a) Cram them all in a day. At least you can tell people back home that you’ve done everything!'
		},
		{
			isCorrect: true,
			text: 'b) Write a list of everything you want to see and then pick your top ones to make the most of'
		},
		{
			isCorrect: false,
			text: 'c) Do what the concierge says'
		},
		{
			isCorrect: false,
			text: 'd) Can’t decide so stay at the hotel. You’ll just have to go to Singapore another time'
		}
	]
},
{
	id: 5,
	questionText: 'The first thing on your list was the Botanic Gardens.  You spot 500SGD lying on the floor. What do you do next?',
	questionMedia: [
		{
			type: 'image',
			url: 'img/Botanic.jpg'
		}
	],
	questionAnswers: [
		{
			isCorrect: false,
			text: 'a) Keep it for yourself, but don’t tell your friend.  Finders Keepers!'
		},
		{
			isCorrect: false,
			text: 'b) Keep it and share it with your friend – what a great piece of luck'
		},
		{
			isCorrect: true,
			text: 'c) Hand it to the nearest member of staff at the gardens'
		},
		{
			isCorrect: false,
			text: 'd) Leave it, I’m sure they’ll come back to look for it'
		}
	]
},
{
	id: 6,
	questionText: 'You’ve been financially rewarded for your honesty and have decided to fly to Bangkok!  On Khao San Road you’re propositioned by a man who’d like you to join him for a “late night show”.  You are reluctant but your friends are eager to get involved. What do you do next?',
	questionMedia: [
		{
			type: 'image',
			url: 'img/Khaosan.jpg'
		}
	],
	questionAnswers: [
		{
			isCorrect: false,
			text: 'a) Let your friend go on their own'
		},
		{
			isCorrect: false,
			text: 'b) Join them even though you have your suspicions – what’s the worst that can happen?!'
		},
		{
			isCorrect: true,
			text: 'c) Convince your friends that you will have a better night if you do something else'
		},
		{
			isCorrect: false,
			text: 'd) Flip a coin and leave it to fate'
		}
	]
},
{
	id: 7,
	questionText: 'The final destination is Brazil where you have been enlisted on an Amazon Jungle Tour with 8 other people. You have been nominated as the leader, and you need to assign roles. What do you do next?',
	questionMedia: [
		{
			type: 'image',
			url: 'img/AsiaOffice.jpg'
		}
	],
	questionAnswers: [
		{
			isCorrect: false,
			text: 'a) Let everyone decide for themselves'
		},
		{
			isCorrect: true,
			text: 'b) Ask everyone to tell you their skills and assign roles'
		},
		{
			isCorrect: false,
			text: 'c) Put everyone’s names in a hat and randomly pick numbers'
		},
		{
			isCorrect: false,
			text: 'd) Don’t assign roles and let everyone do a bit of everything'
		}
	]
}
];

var ImageBackgroundUpdater = function() {

	var updateBackground = function(backgroundContainer, url, context) {
		backgroundContainer.css('background-image', 'url("' + url + '")');
	}

	return {
		UpdateBackground: updateBackground
	}
}

var MagnificVideoPlayer = function() {

	var playVideo = function(url) {
		$.magnificPopup.open({
		  items: {
		    src: url
		  },
		  type: 'iframe'
		});
	}

	return {
		PlayVideo: playVideo
	}
}

var JavascriptSoundPlayer = function(soundFileUrl) {

	var playSound = function() {
		console.debug('playing sound file: ' + soundFileUrl);
		var audio = new Audio(soundFileUrl);
		audio.play()
	}

	return { 
		PlaySound: playSound
	}
}

var VideoBackgroundUpdater = function() {

	var videoPlayer = MagnificVideoPlayer();

	var updateBackground = function(backgroundContainer, url, context) {
		videoPlayer.PlayVideo(url);
	}

	return {
		UpdateBackground: updateBackground
	}
}

var MixedBackgroundUpdater = function() {

	var imageBackgroundUpdater = ImageBackgroundUpdater();
	var videoBackgroundUpdater = VideoBackgroundUpdater();

	var updateBackground = function(backgroundContainer, url, context)  {
		if(context == 'image') {
			console.debug('showing image background');
			imageBackgroundUpdater.UpdateBackground(backgroundContainer, url, context);
		} else if (context == 'video') {
			console.debug('showing video background');
			videoBackgroundUpdater.UpdateBackground(backgroundContainer, url, context);
		}
		else {
			console.debug('Unknown background type: ' + context);
		}
	}

	return {
		UpdateBackground: updateBackground
	}
}

var AlertWrongAnswerHandler = function(wrongAnswerMessage) {

	var handleWrongAnswer = function() {

		alert(wrongAnswerMessage);
	}

	return {
		HandleWrongAnswer: handleWrongAnswer
	}
}

var VideoWrongAnswerHandler = function(wrongAnswerVideoUrl) {

	var videoPlayer = MagnificVideoPlayer();

	var handleWrongAnswer = function() {
		videoPlayer.PlayVideo(wrongAnswerVideoUrl);
	}

	return {
		HandleWrongAnswer: handleWrongAnswer
	}
}

var HighlightQuestionWrongAnswerHandler = function() {


	var handleWrongAnswer = function(context) {
		context.animate({ backgroundColor: '#ff0000 '}, 500);
	}

	return {
		HandleWrongAnswer: handleWrongAnswer
	}
}

var CompositeWrongAnswerHandler = function() {

	var handlers = arguments;

	var handleWrongAnswer = function(context) {
		for(var i in handlers) {
			handlers[i].HandleWrongAnswer(context);
		}
	}

	return {
		HandleWrongAnswer: handleWrongAnswer
	}
}

var SoundWrongAnswerHandler = function(wrongAnswerSoundUrl) {

	var soundPlayer = JavascriptSoundPlayer('snd/wrong.mp3');

	var handleWrongAnswer = function() {
		soundPlayer.PlaySound(wrongAnswerSoundUrl);
	}

	return {
		HandleWrongAnswer: handleWrongAnswer
	}
}

var StaticQuestionProvider = function() {

	var soundPlayer = JavascriptSoundPlayer('snd/right.mp3');

	var getQuestion = function(questionIndex) {

		return questions[questionIndex];
	}

	var testAnswer = function(questionIndex, answerIndex) {
		var isCorrect = questions[questionIndex].questionAnswers[answerIndex].isCorrect
		if(isCorrect) {
			soundPlayer.PlaySound();
		}
		return isCorrect;
	}

	var endOfQuiz = function(index) {
		return (index == questions.length);
	}

	return {
		GetQuestion: getQuestion, 
		TestAnswer: testAnswer,
		EndOfQuiz: endOfQuiz
	}
}

var Quiz = function() {

	var startQuiz;
	var backgroundContainer;
	var questionHeader;
	var questionText;
	var answerContainer;
	var questionProvider;
	var wrongAnswerHandler;
	var backgroundUpdater;
	var currentQuestionIndex;
	var dataAnswerIndex = 'data-answer-index';

	var init = function(options) {

		startQuiz = $(options.startQuizSelector);
		backgroundContainer = $(options.backgroundContainerSelector);
		questionHeader = $(options.questionHeaderSelector);
		questionText = $(options.questionTextSelector);
		answerContainer = $(options.questionAnswerContainerSelector);
		questionProvider = options.questionProvider;
		wrongAnswerHandler = options.wrongAnswerHandler;
		backgroundUpdater = options.backgroundUpdater;
		currentQuestionIndex = 0;	

		startQuiz.on('click', answerClicked);
	}

	var answerClicked = function() {

		var button = $(this);
		var answerIndex = button.attr(dataAnswerIndex);

		if(answerIndex != undefined) {

			var answerIsCorrect = questionProvider.TestAnswer(currentQuestionIndex-1, answerIndex);
			if(!answerIsCorrect) {
				wrongAnswerHandler.HandleWrongAnswer(button);
				return;
			}
		}

		if(questionProvider.EndOfQuiz(currentQuestionIndex)) {
			quizCompleted();
			return;
		}

		var question = questionProvider.GetQuestion(currentQuestionIndex);

		prepareNextQuestion(question);
		
		nextQuestion();
	}	

	var prepareNextQuestion = function(question) {

		questionHeader.text('Question ' + (currentQuestionIndex+1));

		for(var i in question.questionMedia) {
			backgroundUpdater.UpdateBackground(backgroundContainer, question.questionMedia[i].url, question.questionMedia[i].type);
		}

		questionText.text(question.questionText);

		setQuestions(question);
	}

	var setQuestions = function(question) {

		answerContainer.html('');

		for(var i in question.questionAnswers) {
			
			var answer = question.questionAnswers[i];

			createAnswerButton(answerContainer, i, answer);
		}
	}

	var createAnswerButton = function(answerContainer, answerIndex, answer) {

		var questionButton = $('<a href="#" class="btn btn-lg btn-default start-quiz">' + answer.text + '</a>');

		answerContainer.append(questionButton);
		answerContainer.append('<br/><br/>');

		questionButton.attr(dataAnswerIndex, answerIndex);
		questionButton.on('click', answerClicked);
	}

	var quizCompleted = function() {

		document.location = 'congrats.html';
	}

	var nextQuestion = function() {
		currentQuestionIndex++;
	}

	return {
		Init: init
	}

}();

$(function () {

	Quiz.Init({
		startQuizSelector: '.start-quiz',
		backgroundContainerSelector: '.site-wrapper',
		questionHeaderSelector: '.question-header',
		questionTextSelector: '.question-text',
		questionAnswerContainerSelector: '.question-answer-container',
		questionProvider: StaticQuestionProvider(),
		wrongAnswerHandler: CompositeWrongAnswerHandler(SoundWrongAnswerHandler(), HighlightQuestionWrongAnswerHandler()),
		backgroundUpdater: MixedBackgroundUpdater()
	});

});
