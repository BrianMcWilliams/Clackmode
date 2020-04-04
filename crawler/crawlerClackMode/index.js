const express = require('express')
const bodyParser = require('body-parser')
const cors = require('cors')
const puppeteer = require('puppeteer');
const fs = require("fs");
const _ = require('lodash');

var corsOptions = {
	origin: 'http://localhost:4200',
	optionsSuccessStatus: 200
}

const app = express()
app.use(cors(corsOptions))
app.use(bodyParser.json())

let interestCheckUrls = undefined;
let interestChecks = [];

let groupBuyUrls = undefined;
let groupBuys = [];

app.listen(8080, () => {
	console.log('Clackmode-Server started!')
	
	console.log('Grabbing interest check data')
	getDataForInterestChecks();

	// console.log('Grabbing group buy data')
	// getDataForGroupBuys();
})

async function getDataForInterestChecks() {
	
	const browser = await puppeteer.launch({
		args: ['--no-sandbox', '--disable-setuid-sandbox']
	});

	try
	{

		const page = await browser.newPage();
		await page.goto('https://geekhack.org/index.php?board=132.0');
		
		const hrefs = await page.evaluate(
			() => Array.from(document.body.querySelectorAll('a[href]'), ({ href }) => href)
		);

		interestCheckUrls = _.chain(hrefs).filter( (href) => {
		return href.match(new RegExp('(topic=[\\d]*\\.0)$'));
		}).uniq().value();

		let interestCheckTemp = []

		const forLoop = async _ => {
			for (let index = 0; index < interestCheckUrls.length; index++)
			{
				const url = interestCheckUrls[index];

				console.log('Crawling url', url);
				await page.goto(url);
				await page.waitForSelector('title');
			
				const title = await page.title();
				const post = await page.evaluate(() => document.querySelector('#quickModForm > div:nth-child(1) > div > div.postarea > div.post').innerHTML);
				const images = await page.evaluate(() => document.querySelectorAll('#quickModForm > div:nth-child(1) > div > div.postarea > div.post > div.inner > a'));
				const IC = {
					Title: title, 
					Url: url, 
					Post: post,
					Images: images
				};

				if(!interestCheckTemp.includes(IC))
				{
					interestCheckTemp.push(IC);
					console.log('Added interest check :', IC);
					console.log('Interest check count :', interestCheckTemp.length);
				}
				else
				{
					console.log('Skipping already existing IC : ', IC);
				}
			}
		}

		await forLoop.call();

		interestChecks = _.chain(interestCheckTemp).filter( (interestCheck) => {
			return  interestCheck.Title 
						? interestCheck.Title.match(new RegExp('(\\[IC\\])')) 
						: false;
			}).uniq().value();

		console.log('Done getting interest checks')
	}
	catch (e)
	{
		console.log(e);
	}
	finally
	{
		console.log('finally close browser')
		await browser.close();
	}
}


async function getDataForGroupBuys() {
	
	const browser = await puppeteer.launch({
		args: ['--no-sandbox', '--disable-setuid-sandbox']
	});

	try
	{

		const page = await browser.newPage();
		await page.goto('https://geekhack.org/index.php?board=70.0');
		
		const hrefs = await page.evaluate(
			() => Array.from(document.body.querySelectorAll('a[href]'), ({ href }) => href)
		);

		groupBuyUrls = _.chain(hrefs).filter( (href) => {
		return href.match(new RegExp('(topic=[\\d]*\\.0)$'));
		}).uniq().value();

		let groupBuysTemp = []
		const forLoop = async _ => {
			for (let index = 0; index < groupBuyUrls.length; index++)
			{
				const url = groupBuyUrls[index];

				console.log('Crawling url', url);
				await page.goto(url);
				await page.waitForSelector('title');
			
				const title = await page.title();
				const GB = {Title: title, Url: url};

				if(!groupBuysTemp.includes(GB))
				{
					groupBuysTemp.push(GB);
					console.log('Added group buy :', GB);
					console.log('Group buy count :', groupBuysTemp.length);
				}
				else
				{
					console.log('Skipping already existing GB : ', GB);
				}
			}
		}

		await forLoop.call();

		groupBuys = _.chain(groupBuysTemp).filter( (groupBuy) => {
		return groupBuy.Title.match(new RegExp('(\\[GB\\])'));
		}).uniq().value();
	}
	catch (e)
	{
		console.log(e);
	}
	finally
	{
		console.log('finally close browser')
		await browser.close();
	}
}


async function InterestCheckPageCrawl(url, page)
{
	const browser = await puppeteer.launch({
		args: ['--no-sandbox', '--disable-setuid-sandbox']
	});

	try
	{
		const page = await browser.newPage();
		await page.goto(url);
		await page.waitForSelector('title');
	
		const title = await page.title();
		const IC = {Title: title, Url: url};

		if(!interestChecks.includes(IC))
		{
			interestChecks.push(IC);
			console.log('Added interest check :', IC);
			console.log('Interest check count :', interestChecks.length);
		}
		else
		{
			console.log('Skipping already existing IC : ', IC);
		}
	}
	catch (e)
	{
		console.log(e);
	}
	finally
	{
		await browser.close();
	}

}

app.route('/api/interestChecks').get((req, res) => {
	res.status('200').send(interestChecks);
})

app.route('/api/groupBuys').get((req, res) => {
	res.status('200').send(groupBuys);
})