
var questions = [
{
	questionText: 'I want to work for LateRooms because...',
	questionImage: '2.jpg',
	questionAnswers: [
		{
			isCorrect: 'true',
			text: 'AmazeBalls!'
		},
		{
			isCorrect: 'false',
			text: 'Potato'
		},
		{
			isCorrect: 'false',
			text: 'Benefits'
		}
	]
},
{
	questionText: 'Which of these is a LateRooms brand...',
	questionImage: '3.jpg',
	questionAnswers: [
		{
			isCorrect: 'false',
			text: 'AmazeBalls!'
		},
		{
			isCorrect: 'true',
			text: 'MalaPronta',
		},
		{
			isCorrect: 'false',
			text: 'Potato'
		},
		{
			isCorrect: 'false',
			text: 'Benefits'
		}
	]
}
];

var Quiz = function() {

	var startQuiz;
	var imageContainer;
	var questionHeader;
	var questionText;
	var questionAnswerContainer;
	var page;

	var init = function(options) {

		startQuiz = $(options.startQuizSelector);
		imageContainer = $(options.imageContainerSelector);
		questionHeader = $(options.questionHeaderSelector);
		questionText = $(options.questionTextSelector);
		questionAnswerContainer = $(options.questionAnswerContainerSelector);
		page = 0;	

		startQuiz.on('click', answerClicked);
	}

	var answerClicked = function(button) {

		if($(this).hasClass('false')) {
			alert('WRONG!');
			return;
		}

		if(questions.length == page) {
			quizCompleted();
			return;
		}

		var question = questions[page];

		questionHeader.text('Question ' + (page+1));

		var nextImage = 'url("/img/' + question.questionImage + '")';

		imageContainer.css('background-image', nextImage);

		questionText.text(question.questionText);

		setQuestions(question);
		
		page++;
	}	

	var setQuestions = function(question) {

		var answerContainer = $('.question-answer-container');

		answerContainer.html('');

		for(var i in question.questionAnswers) {
			
			var answer = question.questionAnswers[i];

			createAnswerButton(answerContainer, answer);
		}
	}

	var createAnswerButton = function(answerContainer, answer) {

		var questionButton = $('<a href="#" class="btn btn-lg btn-default start-quiz ' + answer.isCorrect + '">' + answer.text + '</a>');

		answerContainer.append(questionButton);
		answerContainer.append('&nbsp;');

		questionButton.on('click', answerClicked);
	}

	var quizCompleted = function() {

		var questionHeader = $('.question-header');
		var questionText = $('.question-text');
		var answerContainer = $('.question-answer-container');

		questionHeader.text('CONGRATULATIONS!!!');

		answerContainer.html('');

		questionText.text('Well done! You are now a LateRooms employee!');
	}

	return {
		Init: init
	}

}();

$(function () {
	
	Quiz.Init({
		startQuizSelector: '.start-quiz',
		imageContainerSelector: 'body',
		questionHeaderSelector: 'question-header',
		questionTextSelector: 'question-text',
		questionAnswerContainerSelector: 'question-answer-container'
	});

});
