import { MearcuryTemplatePage } from './app.po';

describe('Mearcury App', function() {
  let page: MearcuryTemplatePage;

  beforeEach(() => {
    page = new MearcuryTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
