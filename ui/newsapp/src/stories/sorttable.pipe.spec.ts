import { SorttablePipe } from './pipes/sorttable.pipe';

describe('SorttablePipe', () => {
  it('create an instance', () => {
    const pipe = new SorttablePipe();
    expect(pipe).toBeTruthy();
  });
});
