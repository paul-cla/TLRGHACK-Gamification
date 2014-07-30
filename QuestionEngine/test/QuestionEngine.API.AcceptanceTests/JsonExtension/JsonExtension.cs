namespace QuestionEngine.API.AcceptanceTests.JsonExtension
{
    public class Schemas
    {
        public const string HotelVerboseSchema =
		@"{
	        'type':'object',
	        'required':false,
	        'properties':{
		        'appeals': {
			        'type':'array',
			        'required':false,
			        'items':
						        {
							        'type':'object',
							        'required':false,
							        'properties':{
								        'description': {
									        'type':'string',
									        'required':false
								        },
								        'id': {
									        'type':'number',
									        'required':false
								        }
							        }
						        }
			

		        },
		        'cancellation': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'days': {
					        'type':'number',
					        'required':false
				        },
				        'hours': {
					        'type':'string',
					        'required':false
				        },
						'type_name': {
							'type':'string',
							'required':false
						},
						'type': {
							'type':'number',
							'required':false
						}
			        }
		        },
		        'check_in_out': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'check_in': {
					        'type':'string',
					        'required':false
				        },
				        'check_out': {
					        'type':'string',
					        'required':false
				        }
			        }
		        },
		        'city_tax_info': {
			        'type':'string',
			        'required':false
		        },
		        'city_tax': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'type_name': {
					        'type':'string',
					        'required':false
				        }
			        }
		        },
		        'credit_cards': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'payment': {
					        'type':'array',
					        'required':false,
					        'items':
						        {
							        'type':'object',
							        'required':false,
							        'properties':{
								        'description': {
									        'type':'string',
									        'required':false
								        },
								        'id': {
									        'type':'number',
									        'required':false
								        }
							        }
						        }
				        },
				        'reservation': {
					        'type':'array',
					        'required':false,
					        'items':
						        {
							        'type':'object',
							        'required':false,
							        'properties':{
								        'description': {
									        'type':'string',
									        'required':false
								        },
								        'id': {
									        'type':'number',
									        'required':false
								        }
							        }
						        }
				        }
			        }
		        },
		        'currency_code': {
			        'type':'string',
			        'required':false
		        },
		        'description': {
			        'type':'string',
			        'required':false
		        },
		        'directions': {
			        'type':'string',
			        'required':false
		        },
		        'facilities': {
			        'type':'array',
			        'required':false,
			        'items':
						        {
							        'type':'object',
							        'required':false,
							        'properties':{
								        'description': {
									        'type':'string',
									        'required':false
								        },
								        'id': {
									        'type':'number',
									        'required':false
								        }
							        }
						        }
		        },
		        'friendly_name': {
			        'type':'string',
			        'required':false
		        },
		        'id': {
			        'type':'number',
			        'required':true
		        },
                'images':{
		                 'type':'object',
		                 'required':false,
		                 'properties':{
			                'gallery':{
			                   'type':'array',
			                   'required':false,
			                   'items':{
				                  'type':'object',
				                  'required':false,
				                  'properties':{
					                 'url':{
						                'type':'string',
						                'required':false
					                 }
				                  }
			                   }
			                }
		                 }
	                  },
		        'important_info': {
			        'type':'string',
			        'required':false
		        },
		        'location': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'address_line': {
					        'type':'string',
					        'required':false
				        },
				        'city': {
					        'type':'string',
					        'required':false
				        },
				        'country': {
					        'type':'string',
					        'required':false
				        },
				        'county': {
					        'type':'string',
					        'required':false
				        },
				        'lat': {
					        'type':'number',
					        'required':false
				        },
				        'long': {
					        'type':'number',
					        'required':false
				        },
				        'postcode': {
					        'type':'string',
					        'required':false
				        }
			        }
		        },
		        'max_child_age': {
			        'type':'number',
			        'required':true
		        },
		        'name': {
			        'type':'string',
			        'required':false
		        },
		        'number_of_rooms': {
			        'type':'number',
			        'required':false
		        },
		        'rating_accreditor_text': {
			        'type':'string',
			        'required':false
		        },
		        'review_scores': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'review_count': {
					        'type':'number',
					        'required':false
				        },
				        'rounded_average_score': {
					        'type':'number',
					        'required':false
				        }
			        }
		        },
		        'review_summary': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'count': {
					        'type':'number',
					        'required':false
				        },
				        'overall': {
					        'type':'number',
					        'required':false
				        },
				        'spotlight': {
					        'type':'object',
					        'required':false,
					        'properties':{
						        'by': {
							        'type':'string',
							        'required':false
						        },
						        'date': {
							        'type':'string',
							        'required':false
						        },
						        'ratings': {
							        'type':'object',
							        'required':false,
							        'properties':{
								        'overall': {
									        'type':'number',
									        'required':false
								        }
							        }
						        },
						        'text': {
							        'type':'string',
							        'required':false
						        },
						        'title': {
							        'type':'string',
							        'required':false
						        }
					        }
				        }
			        }
		        },
		        'rooms': {
			        'type':'array',
			        'required':false,
			        'items':
				        {
					        'type':'object',
					        'required':false,
					        'properties':{
						        'adults': {
							        'type':'number',
							        'required':false
						        },
						        'bedType': {
							        'type':'string',
							        'required':false
						        },
						        'breakfastIncluded': {
							        'type':'boolean',
							        'required':false
						        },
						        'cancellation': {
							        'type':'object',
							        'required':false,
							        'properties':{
								        'lateCharge': {
									        'type':'number',
									        'required':false
								        },
								        'lateChargedAfterDateLocalTime': {
									        'type':'string',
									        'required':false
								        },
								        'policy': {
									        'type':'string',
									        'required':false
								        }
							        }
						        },
						        'children': {
							        'type':'number',
							        'required':false
						        },
						        'description': {
							        'type':'string',
							        'required':false
						        },
						        'dinnerIncluded': {
							        'type':'boolean',
							        'required':false
						        },
						        'facilities': {
							        'type':'array',
							        'required':false,
							        'items':
								        {
									        'type':'object',
									        'required':false,
									        'properties':{
										        'group_name': {
											        'type':'string',
											        'required':false
										        },
										        'name': {
											        'type':'string',
											        'required':false
										        }
									        }
								        }
							

						        },
						        'id': {
							        'type':'number',
							        'required':false
						        },
						        'key': {
							        'type':'string',
							        'required':false
						        },
						        'minimumNights': {
							        'type':'number',
							        'required':false
						        },
						        'sleeps': {
							        'type':'number',
							        'required':false
						        },
						        'specialOfferDescription': {
							        'type':'string',
							        'required':false
						        },
						        'specialOfferTitle': {
							        'type':'string',
							        'required':false
						        },
						        'typeName': {
							        'type':'string',
							        'required':false
						        },
						        'type': {
							        'type':'number',
							        'required':false
						        }
					        }
				        }
			

		        },
		        'star_accomodation_type': {
			        'type':'string',
			        'required':false
		        },
		        'star_rating': {
			        'type':'object',
			        'required':false,
			        'properties':{
				        'number': {
					        'type':'number',
					        'required':false
				        }
			        }
		        },
		        'typeId': {
			        'type':'number',
			        'required':false
		        },
				'tax_rate': {
						'type':'number',
						'required':false
					}
	        }
        }
        ";


        public const string HotelRatesSchema = @"{
			'type':'object',
			'required':false,
			'properties':{
				'hotelRates': {
					'type':'array',
					'required':false,
					'items':[
						{
							'type':'object',
							'required':false,
							'properties':{
								'bestPrice': {
									'type':'number',
									'required':false
								},
								'best_price_gbp': {
									'type':'number',
									'required':false
								},
								'currency_symbol_local': {
									'type':'string',
									'required':false
								},
								'currency_symbol': {
									'type':'string',
									'required':false
								},
								'currency': {
									'type':'string',
									'required':false
								},
								'date': {
									'type':'string',
									'required':false
								},
								'has_special_offer': {
									'type':'boolean',
									'required':false
								},
								'hide_rack_rate': {
									'type':'boolean',
									'required':false
								},
								'hotelId': {
									'type':'number',
									'required':true
								},
								'hotelPrice': {
									'type':'number',
									'required':false
								},
								'isFull': {
									'type':'boolean',
									'required':true
								},
								'isSpecialOffer': {
									'type':'boolean',
									'required':true
								},
								'number_of_rooms_available': {
									'type':'number',
									'required':false
								},
								'rackRate': {
									'type':'number',
									'required':false
								},
								'rack_rate_gbp': {
									'type':'number',
									'required':false
								},
								'rate_plan_code': {
									'type':'string',
									'required':false
								},
								'roomId': {
									'type':'number',
									'required':true
								},
								'room_type_code': {
									'type':'string',
									'required':false
								},
								'special_offer_type': {
									'type':'string',
									'required':false
								}
							}
						}
					]
				}
			}
		}
		";

    }
}
