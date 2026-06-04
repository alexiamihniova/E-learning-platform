namespace E_learning_platform.Patterns.Iterator
{
    public class CourseModuleIterator : IIterator<CourseModule>
    {
        private CourseModuleCollection _collection;
        private int _currentIndex = 0;

        public CourseModuleIterator(CourseModuleCollection collection)
        {
            _collection = collection;
        }

        public bool HasNext()
        {
            return _currentIndex < _collection.Count;
        }

        public CourseModule? Next()
        {
            if (HasNext())
            {
                return _collection.GetModule(_currentIndex++);
            }
            return null;
        }
    }
}
