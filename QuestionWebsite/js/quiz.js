
var questions = [
{
	questionText: 'I want to work for LateRooms because...',
	questionBackgroundUrl: 'http://www.youtube.com/watch?v=txGfncd5L9U',
	questionBackgroundType: 'video',
	questionAnswers: [
		{
			isCorrect: true,
			text: 'AmazeBalls!'
		},
		{
			isCorrect: false,
			text: 'Potato'
		},
		{
			isCorrect: false,
			text: 'Benefits'
		}
	]
},
{
	questionText: 'Which of these is a LateRooms brand...',
	questionBackgroundUrl: 'img/3.jpg',
	questionBackgroundType: 'image',
	questionAnswers: [
		{
			isCorrect: false,
			text: 'AmazeBalls!'
		},
		{
			isCorrect: true,
			text: 'MalaPronta',
		},
		{
			isCorrect: false,
			text: 'Potato'
		},
		{
			isCorrect: false,
			text: 'Benefits'
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

var StaticQuestionProvider = function() {

	var getQuestion = function(questionIndex) {

		return questions[questionIndex];
	}

	var testAnswer = function(questionIndex, answerIndex) {
		return questions[questionIndex].questionAnswers[answerIndex].isCorrect;
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

		var answerIndex = $(this).attr(dataAnswerIndex);

		if(answerIndex != undefined) {

			var answerIsCorrect = questionProvider.TestAnswer(currentQuestionIndex-1, answerIndex);
			if(!answerIsCorrect) {
				wrongAnswerHandler.HandleWrongAnswer();
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

		backgroundUpdater.UpdateBackground(backgroundContainer, question.questionBackgroundUrl, question.questionBackgroundType);

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
		answerContainer.append('&nbsp;');

		questionButton.attr(dataAnswerIndex, answerIndex);
		questionButton.on('click', answerClicked);
	}

	var quizCompleted = function() {

		questionHeader.text('CONGRATULATIONS!!!');

		answerContainer.html('');

		questionText.text('Well done! You are now a LateRooms employee!');
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
		wrongAnswerHandler: AlertWrongAnswerHandler('WRONG!!'),
		backgroundUpdater: MixedBackgroundUpdater()
	});

});
